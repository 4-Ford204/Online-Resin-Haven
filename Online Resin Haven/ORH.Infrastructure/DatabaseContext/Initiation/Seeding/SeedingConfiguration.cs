using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ORH.Infrastructure.DatabaseContext.Initiation.Seeding
{
    public class SeedingConfiguration<T> : ISeedingConfiguration where T : class
    {
        public required DbSet<T> DbSet { get; set; }
        public required string FileName { get; set; }

        public Type Type => typeof(T);

        public async Task PopulateAsync(JsonSerializerOptions jsonSerializerOptions)
        {
            if (await DbSet.AnyAsync()) return;

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "ORH.Infrastructure", "DatabaseContext", "Initiation", "Data", FileName);

            if (!File.Exists(filePath)) throw new FileNotFoundException(filePath);

            var json = await File.ReadAllTextAsync(filePath);
            var entities = JsonSerializer.Deserialize<List<T>>(json, jsonSerializerOptions);

            if (entities != null)
            {
                await DbSet.AddRangeAsync(entities);
            }
        }
    }
}
