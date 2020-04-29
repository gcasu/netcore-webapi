using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations
{
    /// <summary>
    /// Configures Order entity.
    /// </summary>
    class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasMany(e => e.Products).WithOne().HasForeignKey(e => e.OrderId);
            builder.Property(e => e.Date).HasColumnType("datetime2");
            builder.HasIndex(e => e.Date);
            builder.Property(e => e.CompanyId).IsRequired().HasMaxLength(64);
            builder.HasIndex(e => e.CompanyId);
        }
    }

    /// <summary>
    /// Configures Product entity.
    /// </summary>
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(64);
            builder.Property(e => e.Description).IsRequired().HasMaxLength(128);
            // Ensure uniqueness of the couple name - description
            builder.HasIndex(e => new { e.Name, e.Description }).IsUnique();
        }
    }

    /// <summary>
    /// Configures ProductOrder entity.
    /// </summary>
    public class ProductOrderEntityTypeConfiguration : IEntityTypeConfiguration<ProductOrder>
    {
        public void Configure(EntityTypeBuilder<ProductOrder> builder)
        {
            builder.HasKey(e => new { e.ProductId, e.OrderId });
            builder.HasIndex(e => e.ProductId);
            builder.HasIndex(e => e.OrderId);
        }
    }
}
