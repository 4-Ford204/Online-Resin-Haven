using Customer.API.Abstraction.Installers;
using ORH.Application;
using System.Reflection;

namespace Customer.API.Installers
{
    public class MediatorInstaller : IInstaller
    {
        public void RegisterService(IServiceCollection services, IConfiguration configuration)
        {
            var assemblies = new[] { Assembly.GetAssembly(typeof(ApplicationPoint)) };

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies!));
        }
    }
}
