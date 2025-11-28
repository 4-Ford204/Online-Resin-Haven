using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ORH.Domain.Entities;

namespace ORH.Infrastructure.DatabaseContext.Configurations
{
    public class CharacterConfiguration : IEntityTypeConfiguration<Character>
    {
        public void Configure(EntityTypeBuilder<Character> builder)
        {
            builder.ToTable("Character");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.LineId).IsRequired(false);
            builder.Property(c => c.Name).IsRequired();

            builder.HasOne(c => c.Line)
                .WithMany(l => l.Characters)
                .HasForeignKey(c => c.LineId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
