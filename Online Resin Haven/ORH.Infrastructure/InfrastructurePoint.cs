using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ORH.Infrastructure.Implementation.Shared.Caching;
using ORH.Infrastructure.Implementation.Shared.Messaging;

namespace ORH.Infrastructure
{
    public static class InfrastructurePoint
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCacheService(configuration);

            services.AddMassTransitService(configuration);

            services.AddServices();
        }
    }
}
