using DAL;
using DAL.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;

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
            using (var scope = host.Services.CreateScope())
            {
                var ordersDbContext = scope.ServiceProvider.GetService<OrdersDbContext>();

                if (!ordersDbContext.Companies.Any())
                {
                    ordersDbContext.Companies.AddRange(
                        new Company { Id = "COMPANY_1", Name = "COMPANY_1" },
                        new Company { Id = "COMPANY_2", Name = "COMPANY_2" });

                    ordersDbContext.SaveChanges();
                }
            }
            return host;
        }
    }
}
