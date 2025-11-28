using Customer.API.Abstraction.Installers;
using Microsoft.AspNetCore.Http.Json;
using ORH.Infrastructure;
using System.Text.Json.Serialization;

namespace Customer.API.Installers
{
    public class SystemInstaller : IInstaller
    {
        public void RegisterService(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JsonOptions>(config =>
            {
                config.SerializerOptions.PropertyNamingPolicy = null;
                config.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddInfrastructure(configuration);
        }
    }
}
