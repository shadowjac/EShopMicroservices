using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ordering.Infrastructure.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    { 
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasConversion(orderId => orderId.Value,
                dbId => OrderId.Of(dbId));
        
        builder.Property(p => p.CustomerId)
            .HasConversion(customerId => customerId.Value,
                dbId => CustomerId.Of(dbId));

        builder.ComplexProperty(p => p.OrderName, nameBuilder =>
        {
            nameBuilder.Property(n => n.Value)
                .HasColumnName(nameof(Order.OrderName))
                .HasMaxLength(100)
                .IsRequired();
        });

        builder.OwnsOne(p => p.ShippingAddress, saBuilder =>
        {
            saBuilder.Property(p => p.FirstName)
                .HasMaxLength(50)
                .IsRequired();
            saBuilder.Property(p => p.LastName)
                .HasMaxLength(50)
                .IsRequired();
            saBuilder.Property(p => p.EmailAddress)
                .HasMaxLength(50);
            saBuilder.Property(p => p.AddressLine)
                .HasMaxLength(180)
                .IsRequired();
            saBuilder.Property(p => p.Country)
                .HasMaxLength(50);
            saBuilder.Property(p => p.State)
                .HasMaxLength(50);
            saBuilder.Property(p => p.ZipCode)
                .HasMaxLength(6)
                .IsRequired();
        });
        
        builder.OwnsOne(p => p.BillingAddress, saBuilder =>
        {
            saBuilder.Property(p => p.FirstName)
                .HasMaxLength(50)
                .IsRequired();
            saBuilder.Property(p => p.LastName)
                .HasMaxLength(50)
                .IsRequired();
            saBuilder.Property(p => p.EmailAddress)
                .HasMaxLength(50);
            saBuilder.Property(p => p.AddressLine)
                .HasMaxLength(180)
                .IsRequired();
            saBuilder.Property(p => p.Country)
                .HasMaxLength(50);
            saBuilder.Property(p => p.State)
                .HasMaxLength(50);
            saBuilder.Property(p => p.ZipCode)
                .HasMaxLength(6)
                .IsRequired();
        });

        builder.OwnsOne(p => p.Payment, pBuilder =>
        {
            pBuilder.Property(p => p.CardName)
                .HasMaxLength(50)
                .IsRequired();
            
            pBuilder.Property(p => p.CardNumber)
                .HasMaxLength(24)
                .IsRequired();

            pBuilder.Property(p => p.Expiration)
                .HasMaxLength(10);
            
            pBuilder.Property(p => p.CVV)
                .HasMaxLength(3)
                .IsRequired();

            pBuilder.Property(p => p.PaymentMethod);
        });
        
        builder.Property(p => p.Status)
            .HasDefaultValue(OrderStatus.Draft)
            .HasConversion(status => status.ToString(),
                status => Enum.Parse<OrderStatus>(status));

        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(o => o.CustomerId)
            .IsRequired();

        builder.HasMany(o => o.OrderItems)
            .WithOne()
            .HasForeignKey(oi => oi.OrderId);
    }
}