using System;
using System.Collections.Generic;

namespace DAL.Models
{
    /// <summary>
    /// Order entity data model.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Order progressive identifier.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Date of the order.
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// identifier of the company that made the order.
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// List of ordered products.
        /// </summary>
        public virtual ICollection<ProductOrder> Products { get; set; }
    }

    /// <summary>
    /// Product entity data model.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Product progressive identifier.
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
        /// Stock quantity.
        /// </summary>
        public int Stock { get; set; }
    }

    /// <summary>
    /// ProductOrder entity data model.
    /// </summary>
    public class ProductOrder
    {
        /// <summary>
        /// Product progressive identifier.
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// Order progressive identifier.
        /// </summary>
        public int OrderId { get; set; }
        /// <summary>
        /// Quantity ordered.
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Product unit price.
        /// </summary>
        public double Price { get; set; }
    }
}
