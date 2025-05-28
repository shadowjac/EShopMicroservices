using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ordering.Infrastructure.Data.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Id)
            .HasConversion(orderItemId => orderItemId.Value,
                dbId => OrderItemId.Of(dbId));
        
        builder.Property(p => p.ProductId)
            .HasConversion(value => value.Value,
                dbId => ProductId.Of(dbId));
        
       builder.HasOne<Product>()
           .WithMany()
           .HasForeignKey(oi => oi.ProductId);

       builder.Property(p => p.Quantity)
           .IsRequired();

       builder.Property(p => p.Price)
           .IsRequired();


    }
}