using AutoMapper;
using BLL.CompanyManager;
using BLL.UnitOfWork;
using DAL.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DataTransferObjects;
using WebApi.Exceptions;

namespace WebApi.Extensions
{
    public static class ModelsExtensions
    {
        /// <summary>
        /// Converts and order object to an order data transfer object.
        /// </summary>
        /// <param name="order"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="memoryCache"></param>
        /// <param name="mapper"></param>
        /// <param name="companyManager"></param>
        /// <returns></returns>
        public async static Task<OrderDto> ToDto(this Order order, IUnitOfWork unitOfWork, IMemoryCache memoryCache, IMapper mapper, ICompanyManager companyManager)
        {
            OrderDto orderDto = mapper.Map<OrderDto>(order);
            orderDto.Products = await Task.WhenAll(orderDto.Products.Select(async productOrderDto =>
            {
                if (!memoryCache.TryGetValue(productOrderDto.Id, out Product product))
                    product = await unitOfWork.ProductRepository.GetAsync(productOrderDto.Id);

                if (product == null)
                    throw new ProductNotFoundException(product.Id);

                // Map missing fields name and description
                return mapper.Map(product, productOrderDto);
            }));
            orderDto.Total = companyManager
                .GetTotalStrategy(order.Company)?
                .CalculateTotal(orderDto.Products.Sum(p => p.Price * p.Quantity)) ?? throw new CalculateTotalException(order.Id);

            return orderDto;
        }
    }
}
