using AutoMapper;
using DAL.Models;
using System.Collections.Generic;
using System.Linq;
using WebApi.DataTransferObjects;

namespace WebApi.Extensions
{
    public static class ModelsExtensions
    {
        /// <summary>
        /// Converts an order data entity object to an order data transfer object.
        /// </summary>
        /// <param name="order"></param>
        /// <param name="total"></param>
        /// <param name="products"></param>
        /// <param name="mapper"></param>
        /// <returns></returns>
        public static OrderDto ToDto(this Order order, double total, IEnumerable<Product> products, IMapper mapper)
        {
            OrderDto orderDto = mapper.Map<OrderDto>(order);
            orderDto.Products = orderDto.Products.Select(productOrderDto =>
            {
                Product product = products.Where(p => p != null).FirstOrDefault(p => p.Id.Equals(productOrderDto.Id));

                // Map missing fields name and description
                return product != null ? mapper.Map(product, productOrderDto) : productOrderDto;
            }).ToArray();

            // Set the total
            orderDto.Total = total;

            return orderDto;
        }
    }
}
