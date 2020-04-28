namespace BLL.EvalStrategies.Interfaces
{
    /// <summary>
    /// Interface to calculate the total of an order.
    /// </summary>
    public interface ITotalStrategy
    {
        public double CalculateTotal(double total);
    }
}
