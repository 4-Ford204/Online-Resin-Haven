using Customer.API.Abstraction.Installers;
using Microsoft.EntityFrameworkCore;
using ORH.Infrastructure.DatabaseContext;
using ORH.Infrastructure.DatabaseContext.Initiation.Seeding;

namespace Customer.API.Installers
{
    public class DatabaseInstaller : IInstaller
    {
        public void RegisterService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OnlineResinHaven>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DatabaseContext"));
            });

            var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<OnlineResinHaven>();

            Seeding.InitializeAsync(dbContext).GetAwaiter().GetResult();
        }
    }
}
