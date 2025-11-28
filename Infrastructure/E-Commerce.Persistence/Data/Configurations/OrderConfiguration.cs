using E_Commerce.Domain.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Persistence.Data.Configurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.ShippingAdress, address => address.WithOwner()); // 1=1 relationship

            builder.HasMany(o => o.OrderItems)
                   .WithOne(); // 1=* relationship

            builder.Property(o => o.PaymentStatus)
                   .HasConversion
                   (
                status => status.ToString(), // to the database
                status => Enum.Parse<OrderPaymentStatus>(status) // from the database
                   );

            builder.HasOne(o => o.DeliveryMethod)
                   .WithMany() // * = 1 relationship
                   .OnDelete(DeleteBehavior.SetNull); // If a delivery method is deleted, set the foreign key to null in orders

            builder.Property(o => o.Subtotal)
                   .HasColumnType("decimal(18,3)");
        }
    }
}
