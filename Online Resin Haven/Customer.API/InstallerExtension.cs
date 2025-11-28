using Customer.API.Abstraction.Installers;
using FastEndpoints;
using FastEndpoints.Swagger;

namespace Customer.API
{
    public static class InstallerExtension
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            var installers = typeof(Program).Assembly.ExportedTypes
                .Where(t => typeof(IInstaller).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<IInstaller>()
                .ToList();

            installers.ForEach(installer =>
            {
                installer.RegisterService(services, configuration);
            });
        }

        public static void Configure(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseDefaultExceptionHandler();
            }

            app.UseHttpsRedirection();

            app.UseFastEndpoints(config =>
            {
                config.Endpoints.RoutePrefix = "api";
            });
            app.UseSwaggerGen();
        }
    }
}
