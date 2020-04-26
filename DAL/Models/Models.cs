using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public class Company
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<ProductOrder> Products { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
    }

    public class ProductOrder
    {
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
