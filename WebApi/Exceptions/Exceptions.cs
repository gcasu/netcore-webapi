using System;

namespace WebApi.Exceptions
{
    /// <summary>
    /// Thrown when a product can't be found.
    /// </summary>
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(string message) : base(message) { }
    }
}
