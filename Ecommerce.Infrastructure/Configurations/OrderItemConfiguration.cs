using Ecommerce.Core.Models.OrderAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(i => i.ItemOrdered, io =>
            {
                io.WithOwner();
            });

            builder.Property(i => i.Price)
                .HasColumnType("decimal(18,2)");
        }
    }
}
