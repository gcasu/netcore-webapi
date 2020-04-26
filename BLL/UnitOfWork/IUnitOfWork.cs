using BLL.Repositories.Interfaces;
using System.Threading.Tasks;

namespace BLL.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IProductRepository ProductRepository { get; }
        public ICompanyRepository CompanyRepository { get; }
        public IOrderRepository OrderRepository { get; }
        public IProductOrderRepository ProductOrderRepository { get; }
        Task SaveChangesAsync();
    }
}
