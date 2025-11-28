using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ORH.Domain.Entities;
using ORH.Infrastructure.DatabaseContext.Interceptors;

namespace ORH.Infrastructure.DatabaseContext
{
    public class OnlineResinHaven : DbContext
    {
        public OnlineResinHaven() : base() { }
        public OnlineResinHaven(DbContextOptions<OnlineResinHaven> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Line> Lines { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Studio> Studios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "Customer.API"))
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                var configuration = builder.Build();
                var connectionString = configuration.GetConnectionString("MSSQL");

                optionsBuilder.UseSqlServer(connectionString);
            }

            optionsBuilder.AddInterceptors(new AuditableEntityInterceptor());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OnlineResinHaven).Assembly);
        }
    }
}
