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
using Microsoft.Extensions.Logging;
using WebApi.Exceptions;
using WebApi.Extensions;
using WebApi.ViewModels;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {

        private readonly ILogger<OrdersController> logger;
        private readonly IUnitOfWork unitOfWork;
        private readonly ICompanyManager companyManager;
        private readonly IMemoryCache memoryCache;
        private readonly IMapper mapper;

        public OrdersController(IUnitOfWork unitOfWork, IMemoryCache memoryCache, IMapper mapper, ICompanyManager companyManager, ILogger<OrdersController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.memoryCache = memoryCache;
            this.companyManager = companyManager;
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

            if ((await unitOfWork.CompanyRepository.GetAsync(order.CompanyId)) == null)
                return new BadRequestObjectResult($"Cannot find any company with id {order.CompanyId}.");

            if (await unitOfWork.OrderRepository.ExistsOrderToday(order.CompanyId))
                return new BadRequestObjectResult("You have already done an order today.");

            double total = 0d;
            var updatedProducts = new List<Product>();
            foreach (var productOrder in order.Products)
            {
                if (!memoryCache.TryGetValue(productOrder.ProductId, out Product product))
                    product = await unitOfWork.ProductRepository.GetAsync(productOrder.ProductId);

                if (product == null)
                    throw new ProductNotFoundException(product.Id);

                if (productOrder.Quantity > product.Stock)
                    return new BadRequestObjectResult($"You have exceeded the stock quantity for product {product.Id}: {product.Stock} products left.");

                unitOfWork.ProductRepository.SubtractFromStock(product, productOrder.Quantity);
                updatedProducts.Add(product);

                productOrder.Price = product.Price;
                total += productOrder.Price * productOrder.Quantity;
            }

            if (total < 100)
                return new BadRequestObjectResult("Total spent must be at least equal to 100.");

            try
            {
                await unitOfWork.OrderRepository.AddAsync(order);
                await unitOfWork.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.InnerException?.Message);
            }

            foreach (var product in updatedProducts)
                memoryCache.Set(product.Id, product, TimeSpan.FromHours(1));

            return new OkObjectResult(await order.ToDto(unitOfWork, memoryCache, mapper, companyManager));
        }
    }
}