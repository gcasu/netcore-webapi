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
    public class CompanyRepository : Repository<Company, OrdersDbContext>, ICompanyRepository
    {
        public CompanyRepository(OrdersDbContext context) : base(context) { }
    }

    public class OrderRepository : Repository<Order, OrdersDbContext>, IOrderRepository
    {
        public OrderRepository(OrdersDbContext context) : base(context) { }

        public async Task<bool> ExistsOrderToday(string companyId)
        {
            DateTime today = DateTime.Today;
            return await Entities.AnyAsync(o => o.CompanyId.Equals(companyId) && o.Date.Date == today);
        }

        public override async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await Entities.Include(e => e.Company).Include(e => e.Products).ToArrayAsync();
        }

        public override async Task<IEnumerable<Order>> GetPagedResultsAsync(int pageIndex, int pageSize)
        {
            var pageResults = pageIndex > 1 ?
                Entities.Skip((pageIndex - 1) * pageSize).Take(pageSize) :
                Entities.Take(pageSize);

            return await pageResults.Include(e => e.Company).Include(e => e.Products).ToArrayAsync();
        }
    }

    public class ProductOrderRepository : Repository<ProductOrder, OrdersDbContext>, IProductOrderRepository
    {
        public ProductOrderRepository(OrdersDbContext context) : base(context) { }
    }

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
