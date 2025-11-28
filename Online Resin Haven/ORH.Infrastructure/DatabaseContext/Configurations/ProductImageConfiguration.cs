using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ORH.Domain.Entities;

namespace ORH.Infrastructure.DatabaseContext.Configurations
{
    class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("ProductImage");

            builder.HasKey(pi => pi.Id);

            builder.Property(pi => pi.Id).ValueGeneratedOnAdd();
            builder.Property(pi => pi.ProductId).IsRequired(false);
            builder.Property(pi => pi.URL).IsRequired();

            builder.HasOne(pi => pi.Product)
                .WithMany(p => p.ProductImages)
                .HasForeignKey(pi => pi.ProductId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
