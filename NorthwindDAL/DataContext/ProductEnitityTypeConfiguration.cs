using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthwindModels.Models;

namespace NorthwindDAL.DataContext
{
    public class ProductEnitityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id).HasName(nameof(Product.Id));
            builder.Property(p => p.Id).HasColumnName("ProductId");

            builder.Property(p => p.ProductName).IsRequired().HasMaxLength(40);

            builder.Property(p => p.QuantityPerUnit).HasMaxLength(20);
        }
    }
}
