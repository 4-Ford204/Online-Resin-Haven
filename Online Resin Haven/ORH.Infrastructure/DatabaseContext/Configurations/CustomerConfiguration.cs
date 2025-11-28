using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ORH.Domain.Entities;

namespace ORH.Infrastructure.DatabaseContext.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.FirstName).IsRequired();
            builder.Property(c => c.LastName).IsRequired();
            builder.Property(c => c.Email).IsRequired();
            builder.Property(c => c.Password).IsRequired();
            builder.Property(c => c.Gender).IsRequired();
            builder.Property(c => c.Phone).IsRequired(false);
            builder.Property(c => c.Address).IsRequired(false);
        }
    }
}
