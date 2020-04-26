using BLL.EvalStrategies.Interfaces;
using DAL.Models;

namespace BLL.CompanyManager
{
    public interface ICompanyManager
    {
        public ITotalStrategy GetTotalStrategy(Company company);
    }
}
