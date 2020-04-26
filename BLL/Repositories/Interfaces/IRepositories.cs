using DAL.Models;
using System.Threading.Tasks;

namespace BLL.Repositories.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        public void SubtractFromStock(Product product, int quantity);
    }

    public interface IProductOrderRepository : IRepository<ProductOrder>
    {
    }

    public interface IOrderRepository : IRepository<Order>
    {
        public Task<bool> ExistsOrderToday(string companyId);
    }

    public interface ICompanyRepository : IRepository<Company>
    {
    }
}
