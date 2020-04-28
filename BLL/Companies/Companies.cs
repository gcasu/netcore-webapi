using BLL.EvalStrategies;

namespace BLL.Companies
{
    /// <summary>
    /// Company 1 in the system.
    /// </summary>
    public class Company_1 : AbstractCompany
    {
        public Company_1()
        {
            CompanyId = "COMPANY_1";
            CompanyName = "COMPANY_1";
            TotalStrategy = new ManagementCostTotalStrategy();
        }
    }

    /// <summary>
    /// Company 2 in the system.
    /// </summary>
    public class Company_2 : AbstractCompany
    {
        public Company_2()
        {
            CompanyId = "COMPANY_2";
            CompanyName = "COMPANY_2";
            TotalStrategy = new AdministrativeCostTotalStrategy();
        }
    }
}
