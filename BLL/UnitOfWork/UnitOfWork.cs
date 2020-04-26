using BLL.Repositories;
using BLL.Repositories.Interfaces;
using DAL;
using System;
using System.Threading.Tasks;

namespace BLL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly OrdersDbContext ordersDbContext;
        private readonly ProductsDbContext productsDbContext;

        public UnitOfWork(OrdersDbContext ordersDbContext, ProductsDbContext productsDbContext)
        {
            this.ordersDbContext = ordersDbContext;
            this.productsDbContext = productsDbContext;
        }

        private IProductRepository _ProductRepository;
        public IProductRepository ProductRepository { get => _ProductRepository ?? (_ProductRepository = new ProductRepository(productsDbContext)); }

        private ICompanyRepository _CompanyRepository;
        public ICompanyRepository CompanyRepository { get => _CompanyRepository ?? (_CompanyRepository = new CompanyRepository(ordersDbContext)); }

        private IOrderRepository _OrderRepository;
        public IOrderRepository OrderRepository { get => _OrderRepository ?? (_OrderRepository = new OrderRepository(ordersDbContext)); }

        private IProductOrderRepository _ProductOrderRepository;
        public IProductOrderRepository ProductOrderRepository { get => _ProductOrderRepository ?? (_ProductOrderRepository = new ProductOrderRepository(ordersDbContext)); }

        public async Task SaveChangesAsync()
        {
            await ordersDbContext.SaveChangesAsync();
            await productsDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            ordersDbContext.Dispose();
            productsDbContext.Dispose();
        }
    }
}
