using Microsoft.EntityFrameworkCore;
using ORH.Infrastructure;
using ORH.Infrastructure.DatabaseContext;
using ORH.Application;
using System.Reflection;

namespace Product.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;
            var configuration = builder.Configuration;

            #region SERVICES

            services.AddControllers();

            services.AddOpenApi();

            var assemblies = new[] { Assembly.GetAssembly(typeof(ApplicationPoint)) };
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies!));

            services.AddDbContext<OnlineResinHaven>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DatabaseContext"));
            });

            services.AddInfrastructure(configuration);

            #endregion

            var app = builder.Build();

            #region APP

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            #endregion

            app.Run();
        }
    }
}
