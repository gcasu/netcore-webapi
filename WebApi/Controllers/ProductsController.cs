﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using BLL.UnitOfWork;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using WebApi.Extensions;
using WebApi.ViewModels;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> logger;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMemoryCache memoryCache;
        private readonly IMapper mapper;

        public ProductsController(IUnitOfWork unitOfWork, IMemoryCache memoryCache, IMapper mapper, ILogger<ProductsController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.memoryCache = memoryCache;
            this.mapper = mapper;
            this.logger = logger;
        }

        /// <summary>
        /// Adds a new product to the store.
        /// </summary>
        /// <param name="viewModel">Product info.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddAsync(ProductViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return new BadRequestObjectResult(ModelState.Errors());

            Product product = mapper.Map<Product>(viewModel);

            try
            {
                await unitOfWork.ProductRepository.AddAsync(product);
                await unitOfWork.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.InnerException?.Message);
            }

            memoryCache.Set(product.Id, product, TimeSpan.FromHours(1));

            return new OkObjectResult(product);
        }

        /// <summary>
        /// Returns all the products in the store.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var results = await unitOfWork.ProductRepository.GetAllAsync();

            return new OkObjectResult(results);
        }

        /// <summary>
        /// Returns a single product from the store.
        /// </summary>
        /// <param name="id">Product progressive identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            if (!memoryCache.TryGetValue(id, out Product product))
                product = await unitOfWork.ProductRepository.GetAsync(id);

            if (product == null)
                return new NotFoundObjectResult($"Cannot find product with id {id}.");

            return new OkObjectResult(product);
        }
    }
}