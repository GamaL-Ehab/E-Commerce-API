using E_Commerce.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Persistence.Context.Configurations
{
    public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(d => d.ShortName).HasColumnType("varchar").HasMaxLength(128);
            builder.Property(d => d.Description).HasColumnType("varchar").HasMaxLength(256);
            builder.Property(d => d.DeliveryTime).HasColumnType("varchar").HasMaxLength(128);
            builder.Property(d => d.Price).HasColumnType("decimal(18,2)");
        }
    }
}
