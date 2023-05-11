using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthwindModels.Models;

namespace NorthwindDAL.DataContext
{
    public class CustomerEnitityTypeConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.CustomerID).HasName(nameof(Customer.CustomerID));
            builder.Property(c => c.CustomerID).IsRequired().HasMaxLength(5).IsFixedLength(true);

            builder.Property(c => c.CompanyName).IsRequired().HasMaxLength(40);

            builder.Property(c => c.ContactName).HasMaxLength(30);

            builder.Property(c => c.ContactTitle).HasMaxLength(30);

            builder.Property(c => c.Address).HasMaxLength(60);

            builder.Property(c => c.City).HasMaxLength(15);

            builder.Property(c => c.Region).HasMaxLength(15);

            builder.Property(c => c.PostalCode).HasMaxLength(10);

            builder.Property(c => c.Country).HasMaxLength(15);

            builder.Property(c => c.Phone).HasMaxLength(24);

            builder.Property(c => c.Fax).HasMaxLength(24);
        }
    }
}
