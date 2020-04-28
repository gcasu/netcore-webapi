using BLL.Companies;

namespace BLL.CompanyManager
{
    /// <summary>
    /// Factory class that manage the companies in the system.
    /// </summary>
    public class CompanyManager : ICompanyManager
    {
        /// <summary>
        /// Returns a company in the system by id.
        /// </summary>
        /// <param name="companyId">Company identifier.</param>
        /// <returns>The company identified by companyId.</returns>
        public AbstractCompany GetCompany(string companyId)
        {
            return companyId switch
            {
                "COMPANY_1" => new Company_1(),
                "COMPANY_2" => new Company_2(),
                _ => null,
            };
        }
    }
}
