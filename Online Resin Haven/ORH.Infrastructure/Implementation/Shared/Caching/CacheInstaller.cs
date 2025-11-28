using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ORH.Application.Interface.Shared.Caching;
using StackExchange.Redis;

namespace ORH.Infrastructure.Implementation.Shared.Caching
{
    public static class CacheInstaller
    {
        public static void AddCacheService(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("CacheConfiguration");

            if (section.Exists())
            {
                var enabled = section.GetValue<bool>("Enabled");

                if (enabled)
                {
                    var provider = section.GetValue<string>("Provider");

                    switch (provider)
                    {
                        case "Redis":
                            var redis = section.GetSection("Redis");
                            var connectionString = redis.GetValue<string>("ConnectionString");

                            services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(connectionString!));
                            services.AddStackExchangeRedisCache(options => options.Configuration = connectionString);
                            services.AddSingleton<ICacheService, RedisCacheService>();

                            return;
                        default:
                            break;
                    }
                }
            }

            services.AddSingleton<ICacheService, NoCacheService>();
        }
    }
}
