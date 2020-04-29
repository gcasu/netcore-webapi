using DAL.Models;
using System;
using System.Threading.Tasks;

namespace BLL.Repositories.Interfaces
{
    /// <summary>
    /// IRepository for entity type Product.
    /// </summary>
    public interface IProductRepository : IRepository<Product>
    {
        /// <summary>
        /// Subtracts the specified quantity from the stock.
        /// </summary>
        /// <param name="product">A product.</param>
        /// <param name="quantity">Quantity ordered.</param>
        public void SubtractFromStock(Product product, int quantity);
    }

    /// <summary>
    /// IRepository for entity type ProductOrder.
    /// </summary>
    public interface IProductOrderRepository : IRepository<ProductOrder>
    {
    }

    /// <summary>
    /// IRepository for entity type Order.
    /// </summary>
    public interface IOrderRepository : IRepository<Order>
    {
        /// <summary>
        /// Checks whether exists an order done in the specified date for the specified company.
        /// </summary>
        /// <param name="companyId">Identifier of the company.</param>
        /// <param name="date">Date to check for an order.</param>
        /// <returns>True if exists, false otherwise.</returns>
        public Task<bool> ExistsOrderInDate(string companyId, DateTime date);
    }
}
