using BLL.Repositories.Interfaces;
using System.Threading.Tasks;

namespace BLL.UnitOfWork
{
    /// <summary>
    /// Allows to access all the entities repositories.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Products repository.
        /// </summary>
        public IProductRepository ProductRepository { get; }
        /// <summary>
        /// Order repository.
        /// </summary>
        public IOrderRepository OrderRepository { get; }
        /// <summary>
        /// ProductOrders repository.
        /// </summary>
        public IProductOrderRepository ProductOrderRepository { get; }
        /// <summary>
        /// Commits changes to Products database.
        /// </summary>
        /// <returns></returns>
        Task SaveProductsAsync();
        /// <summary>
        /// Commits changes to Orders database, keeping Products database updated.
        /// </summary>
        /// <returns></returns>
        Task SaveOrdersAsync();
    }
}
