using ORH.Domain.Entities;
using System.Text.Json;

namespace ORH.Infrastructure.DatabaseContext.Initiation.Seeding
{
    public static class Seeding
    {
        public static async Task InitializeAsync(OnlineResinHaven dbContext)
        {
            var tables = new List<object>()
            {
                new SeedingConfiguration<Customer>
                {
                    DbSet = dbContext.Customers,
                    FileName = "customer.json"
                },
                new SeedingConfiguration<Studio>
                {
                    DbSet = dbContext.Studios,
                    FileName = "studio.json"
                },
                new SeedingConfiguration<Line>
                {
                    DbSet = dbContext.Lines,
                    FileName = "line.json"
                },
                new SeedingConfiguration<Character>
                {
                    DbSet = dbContext.Characters,
                    FileName = "character.json"
                },
                new SeedingConfiguration<Product>
                {
                    DbSet = dbContext.Products,
                    FileName = "product.json"
                },
                new SeedingConfiguration<ProductImage>
                {
                    DbSet = dbContext.ProductImages,
                    FileName = "product_image.json"
                }
            };
            var jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            foreach (var table in tables.Cast<ISeedingConfiguration>())
            {
                await table.PopulateAsync(jsonSerializerOptions);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
