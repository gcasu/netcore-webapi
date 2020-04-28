using BLL.EvalStrategies.Interfaces;

namespace BLL.Companies
{
    /// <summary>
    /// Represents an abstract company in the system.
    /// </summary>
    public abstract class AbstractCompany
    {
        /// <summary>
        /// Company identifier.
        /// </summary>
        public string CompanyId { get; protected set; }
        /// <summary>
        /// Company name.
        /// </summary>
        public string CompanyName { get; protected set; }
        /// <summary>
        /// Strategy to apply to calculate the total of an order.
        /// </summary>
        public ITotalStrategy TotalStrategy { get; protected set; }
    }
}
