using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthwindModels.Models;

namespace NorthwindDAL.DataContext
{
    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id).HasName(nameof(Order.Id));
            builder.Property(o => o.Id).HasColumnName("OrderID");

            builder.Property(o => o.CustomerID).HasMaxLength(5).IsFixedLength(true);

            builder.Property(o => o.ShipName).HasMaxLength(40);

            builder.Property(o => o.ShipAddress).HasMaxLength(60);

            builder.Property(o => o.ShipCity).HasMaxLength(15);

            builder.Property(o => o.ShipRegion).HasMaxLength(15);

            builder.Property(o => o.ShipPostalCode).HasMaxLength(10);

            builder.Property(o => o.ShipCountry).HasMaxLength(15);
        }
    }
}
