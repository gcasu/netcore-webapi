using BLL.Companies;

namespace BLL.CompanyManager
{

    /// <summary>
    /// Factory interface that manage the companies in the system.
    /// </summary>
    public interface ICompanyManager
    {
        /// <summary>
        /// Returns a company in the system by id.
        /// </summary>
        /// <param name="companyId">Company identifier.</param>
        /// <returns>The company identified by companyId.</returns>
        public AbstractCompany GetCompany(string companyId);
    }
}
