using BLL.EvalStrategies;
using BLL.EvalStrategies.Interfaces;
using DAL.Models;

namespace BLL.CompanyManager
{
    public class CompanyManager : ICompanyManager
    {
        public ITotalStrategy GetTotalStrategy(Company company)
        {
            return company.Id switch
            {
                "COMPANY_1" => new ManagementCostTotalStrategy(),
                "COMPANY_2" => new AdministrativeCostTotalStrategy(),
                _ => null,
            };
        }
    }
}
