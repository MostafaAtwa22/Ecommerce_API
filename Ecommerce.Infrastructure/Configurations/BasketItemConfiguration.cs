using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Configurations
{
    public class BasketItemConfiguration : IEntityTypeConfiguration<BasketItem>
    {
        public void Configure(EntityTypeBuilder<BasketItem> builder)
        {
            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            builder.HasOne(b => b.CustomerBasket)
                .WithMany(b => b.Items)
                .HasForeignKey(p => p.CustomerBasketId);
        }
    }
}
