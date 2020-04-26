using System;

namespace WebApi.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(int productId) : base($"Cannot find product with id {productId}.") { }
    }

    public class CalculateTotalException : Exception
    {
        public CalculateTotalException(int orderId) : base($"Cannot calculate total for order {orderId}.") { }
    }
}
