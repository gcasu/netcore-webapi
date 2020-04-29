using BLL.Repositories.Interfaces;
using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Repositories
{
    /// <summary>
    /// Concrete repository for entity type Order.
    /// </summary>
    public class OrderRepository : Repository<Order, OrdersDbContext>, IOrderRepository
    {
        public OrderRepository(OrdersDbContext context) : base(context) { }

        public async Task<bool> ExistsOrderInDate(string companyId, DateTime date)
        {
            return await Entities.AnyAsync(o => o.CompanyId.Equals(companyId) && o.Date.Date == date.Date);
        }

        public override async Task<IEnumerable<Order>> GetAllAsync()
        {
            // Load related entities
            return await Entities.Include(e => e.Products).ToArrayAsync();
        }

        public override async Task<IEnumerable<Order>> GetPagedResultsAsync(int pageIndex, int pageSize)
        {
            var pageResults = pageIndex > 1 ?
                Entities.Skip((pageIndex - 1) * pageSize).Take(pageSize) :
                Entities.Take(pageSize);

            // Load related entities
            return await pageResults.Include(e => e.Products).ToArrayAsync();
        }
    }

    /// <summary>
    /// Concrete repository for entity type ProductOrder.
    /// </summary>
    public class ProductOrderRepository : Repository<ProductOrder, OrdersDbContext>, IProductOrderRepository
    {
        public ProductOrderRepository(OrdersDbContext context) : base(context) { }
    }

    /// <summary>
    /// Concrete repository for entity type Product.
    /// </summary>
    public class ProductRepository : Repository<Product, ProductsDbContext>, IProductRepository
    {
        public ProductRepository(ProductsDbContext context) : base(context) { }

        public void SubtractFromStock(Product product, int quantity)
        {
            product.Stock -= quantity;
            DbContext.Update(product);
        }
    }
}
