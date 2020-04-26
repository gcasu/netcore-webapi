using BLL.EvalStrategies.Interfaces;

namespace BLL.EvalStrategies
{
    public class AdministrativeCostTotalStrategy : ITotalStrategy
    {
        public double CalculateTotal(double total) => total + 1;
    }

    public class ManagementCostTotalStrategy : ITotalStrategy
    {
        public double CalculateTotal(double total) => total + total * 0.0002;
    }
}
