using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ORH.Domain.Entities;

namespace ORH.Infrastructure.DatabaseContext.Configurations
{
    class LineConfiguration : IEntityTypeConfiguration<Line>
    {
        public void Configure(EntityTypeBuilder<Line> builder)
        {
            builder.ToTable("Line");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Id).ValueGeneratedOnAdd();
            builder.Property(l => l.Name).IsRequired();
        }
    }
}
