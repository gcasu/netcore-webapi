using BLL.Repositories;
using BLL.Repositories.Interfaces;
using DAL;
using System;
using System.Threading.Tasks;

namespace BLL.UnitOfWork
{
    /// <summary>
    /// Allows to access all the entities repositories.
    /// </summary>
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

        private IOrderRepository _OrderRepository;
        public IOrderRepository OrderRepository { get => _OrderRepository ?? (_OrderRepository = new OrderRepository(ordersDbContext)); }

        private IProductOrderRepository _ProductOrderRepository;
        public IProductOrderRepository ProductOrderRepository { get => _ProductOrderRepository ?? (_ProductOrderRepository = new ProductOrderRepository(ordersDbContext)); }

        public async Task SaveProductsAsync()
        {
            // Start a new transaction for the products
            var productsTransaction = await productsDbContext.Database.BeginTransactionAsync();
            try
            {
                // Save all changes to the database
                await productsDbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                // Discard all changes if something fails
                await productsTransaction.RollbackAsync();
                throw e;
            }

            // Commit all changes if saved successfully
            await productsTransaction.CommitAsync();
        }

        public async Task SaveOrdersAsync()
        {
            // Start a new transaction for the orders
            var ordersTransaction = await ordersDbContext.Database.BeginTransactionAsync();
            try
            {
                // Save all changes to the database
                await ordersDbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                // Discard all changes if something fails
                await ordersTransaction.RollbackAsync();
                throw e;
            }

            // Start a new transaction for the products
            var productsTransaction = await productsDbContext.Database.BeginTransactionAsync();
            try
            {
                // Save all changes to the database
                await productsDbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                // Discard all changes if something fails
                await productsTransaction.RollbackAsync();
                await ordersTransaction.RollbackAsync();
                throw e;
            }

            // Commit all changes if saved successfully
            await Task.WhenAll(ordersTransaction.CommitAsync(), productsTransaction.CommitAsync());
        }

        public void Dispose()
        {
            ordersDbContext.Dispose();
            productsDbContext.Dispose();
        }
    }
}
