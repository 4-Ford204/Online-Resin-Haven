using Customer.API.Abstraction.Installers;
using FastEndpoints;
using FastEndpoints.Swagger;

namespace Customer.API.Installers
{
    public class FastEndpointInstaller : IInstaller
    {
        public void RegisterService(IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddFastEndpoints()
                .SwaggerDocument(config =>
                {
                    config.DocumentSettings = settings =>
                    {
                        settings.Title = "Customer API";
                        settings.Version = "v1";
                    };
                });
        }
    }
}
