using Microsoft.Extensions.Hosting;

namespace WebApi.Extensions
{
    public static class WebHostExtensions
    {
        /// <summary>
        /// Seed database with basic data.
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public static IHost Seed(this IHost host)
        {
            //using (var scope = host.Services.CreateScope())
            //{
            //    var ordersDbContext = scope.ServiceProvider.GetService<OrdersDbContext>();
            //    var productsDbContext = scope.ServiceProvider.GetService<ProductsDbContext>();
            //}
            return host;
        }
    }
}
