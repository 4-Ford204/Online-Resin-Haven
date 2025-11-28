using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ORH.Domain.Entities;

namespace ORH.Infrastructure.DatabaseContext.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.CharacterId).IsRequired(false);
            builder.Property(p => p.StudioId).IsRequired(false);
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Quantity).HasDefaultValue(0);
            builder.Property(p => p.Price).IsRequired();

            builder.HasOne(p => p.Character)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CharacterId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.Studio)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.StudioId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
