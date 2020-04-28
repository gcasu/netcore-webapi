using BLL.EvalStrategies.Interfaces;

namespace BLL.EvalStrategies
{
    /// <summary>
    /// Concrete strategy to calculate the total of an order.
    /// </summary>
    public class AdministrativeCostTotalStrategy : ITotalStrategy
    {
        public double CalculateTotal(double total) => total + 1;
    }

    /// <summary>
    /// Concrete strategy to calculate the total of an order.
    /// </summary>
    public class ManagementCostTotalStrategy : ITotalStrategy
    {
        public double CalculateTotal(double total) => total + total * 0.0002;
    }
}
