using System.ComponentModel.DataAnnotations;

namespace WebApi.ViewModels
{
    /// <summary>
    /// Order view model.
    /// </summary>
    public class OrderViewModel
    {
        /// <summary>
        /// Company code.
        /// </summary>
        [Required, StringLength(64)]
        public string CompanyId { get; set; }
        /// <summary>
        /// List of ordered products.
        /// </summary>
        [Required, MinLength(1)]
        public ProductOrderViewModel[] Products { get; set; }
    }

    /// <summary>
    /// Product Order view model.
    /// </summary>
    public class ProductOrderViewModel
    {
        /// <summary>
        /// Product progressive identifier.
        /// </summary>
        [Required]
        public int ProductId { get; set; }
        /// <summary>
        /// Product order quantity.
        /// </summary>
        [Required, Range(1, 1000)]
        public int Quantity { get; set; }
    }

    /// <summary>
    /// Product view model.
    /// </summary>
    public class ProductViewModel
    {
        /// <summary>
        /// Product name.
        /// </summary>
        [Required, StringLength(64)]
        public string Name { get; set; }
        /// <summary>
        /// Product description.
        /// </summary>
        [Required, StringLength(128)]
        public string Description { get; set; }
        /// <summary>
        /// Product unit price.
        /// </summary>
        [Required, Range(0.01, 1000)]
        public double Price { get; set; }
        /// <summary>
        /// Product stock quantity.
        /// </summary>
        [Required, Range(1, 1000)]
        public int Stock { get; set; }
    }
}
