using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DAL.Configurations
{
    public class CompanyEntityTypeConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasMaxLength(64);
            builder.Property(e => e.Name).HasMaxLength(128);
        }
    }

    class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasOne(e => e.Company).WithMany().HasForeignKey(e => e.CompanyId);
            builder.HasMany(e => e.Products).WithOne().HasForeignKey(e => e.OrderId);
            builder.Property(e => e.Date).HasColumnType("datetime2").HasDefaultValue(DateTime.Now);
            builder.HasIndex(e => e.Date);
            builder.Property(e => e.CompanyId).IsRequired().HasMaxLength(64);
            builder.HasIndex(e => e.CompanyId);
        }
    }

    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(64);
            builder.Property(e => e.Description).IsRequired().HasMaxLength(128);
            builder.HasIndex(e => new { e.Name, e.Description }).IsUnique();
        }
    }

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
