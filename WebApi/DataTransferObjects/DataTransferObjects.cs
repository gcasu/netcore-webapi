using System;

namespace WebApi.DataTransferObjects
{
    /// <summary>
    /// Order data transfer object.
    /// </summary>
    public class OrderDto
    {
        /// <summary>
        /// Order progressive identifier.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Total amount of the order.
        /// </summary>
        public double Total { get; set; }
        /// <summary>
        /// Date of the order.
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Identifier of the company that made the order.
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// List of ordered products.
        /// </summary>
        public ProductOrderDto[] Products { get; set; }
    }

    /// <summary>
    /// Product order data transfer object.
    /// </summary>
    public class ProductOrderDto
    {
        /// <summary>
        /// Product order progressive identifier.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Product name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Product description.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Product unit price.
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// Produce order quantity.
        /// </summary>
        public int Quantity { get; set; }
    }
}
