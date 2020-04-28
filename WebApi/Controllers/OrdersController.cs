using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.CompanyManager;
using BLL.UnitOfWork;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebApi.Exceptions;
using WebApi.Extensions;
using WebApi.ViewModels;
using static WebApi.Strings.Strings;

namespace WebApi.Controllers
{
    /// <summary>
    /// Exposes Orders resource.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {

        private readonly ILogger<OrdersController> logger;
        private readonly IUnitOfWork unitOfWork;
        private readonly ICompanyManager companyManager;
        private readonly IConfiguration configuration;
        private readonly IMemoryCache memoryCache;
        private readonly IMapper mapper;

        public OrdersController(IUnitOfWork unitOfWork, IMemoryCache memoryCache, IMapper mapper, ICompanyManager companyManager, IConfiguration configuration, ILogger<OrdersController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.memoryCache = memoryCache;
            this.companyManager = companyManager;
            this.configuration = configuration;
            this.mapper = mapper;
            this.logger = logger;
        }

        /// <summary>
        /// Returns a paged  list of orders.
        /// </summary>
        /// <param name="pageIndex">Page index.</param>
        /// <param name="pageSize">Page size.</param>
        /// <returns></returns>
        [HttpGet("{pageIndex}/{pageSize}")]
        public async Task<IActionResult> GetPagedResultsAsync(int pageIndex, int pageSize)
        {
            var results = await Task.WhenAll((await unitOfWork.OrderRepository.GetPagedResultsAsync(pageIndex, pageSize))
                .Select(async order => await order.ToDto(unitOfWork, memoryCache, mapper, companyManager)));

            return new OkObjectResult(results);
        }

        /// <summary>
        /// Adds a new order for a company.
        /// </summary>
        /// <param name="viewModel">Order info.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddAsync(OrderViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return new BadRequestObjectResult(ModelState.Errors());

            Order order = mapper.Map<Order>(viewModel);
            order.Date = DateTime.Now;

            // Check if company exists
            if (companyManager.GetCompany(order.CompanyId) == null)
                return new BadRequestObjectResult(string.Format(CompanyNotFound, order.CompanyId));

            // Check if exists another order today
            if (await unitOfWork.OrderRepository.ExistsOrderToday(order.CompanyId))
                return new BadRequestObjectResult(OrdersExceeded);

            double total = 0d;
            var updatedProducts = new List<Product>(); // Store the products to update later
            foreach (var productOrder in order.Products)
            {
                // Get the product from cache if exists, otherwise query the database
                if (!memoryCache.TryGetValue(productOrder.ProductId, out Product product))
                    product = await unitOfWork.ProductRepository.GetAsync(productOrder.ProductId);

                // Return an error if the product does not exist
                if (product == null)
                    throw new ProductNotFoundException(string.Format(ProductNotFound, productOrder.ProductId));

                // Quantity must be available
                if (productOrder.Quantity > product.Stock)
                    return new BadRequestObjectResult(string.Format(StockExceeded, product.Id, product.Stock));

                // Update product stock
                unitOfWork.ProductRepository.SubtractFromStock(product, productOrder.Quantity);
                updatedProducts.Add(product);

                // Calculate partial total
                productOrder.Price = product.Price;
                total += productOrder.Price * productOrder.Quantity;
            }

            // Check the minimum total spent
            double minTotalSpent = double.Parse(configuration["Order:MinimumTotalSpent"]);
            if (total < minTotalSpent)
                return new BadRequestObjectResult(string.Format(MinimumTotalSpent, minTotalSpent));

            try
            {
                await unitOfWork.OrderRepository.AddAsync(order);
                await unitOfWork.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new BadRequestObjectResult(OrderCreationFailed);
            }

            double absExpiration = double.Parse(configuration["Cache:AbsoluteExpiration"]);
            foreach (var product in updatedProducts)
                memoryCache.Set(product.Id, product, TimeSpan.FromMinutes(absExpiration));

            return new OkObjectResult(await order.ToDto(unitOfWork, memoryCache, mapper, companyManager));
        }
    }
}