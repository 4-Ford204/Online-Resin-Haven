using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ORH.Infrastructure.Implementation.Shared.Messaging
{
    public static class MassTransitInstaller
    {
        public static void AddMassTransitService(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("MassTransitConfiguration");

            if (section.Exists())
            {
                var enabled = section.GetValue<bool>("Enabled");

                if (enabled)
                {
                    var provider = section.GetValue<string>("Provider");

                    switch (provider)
                    {
                        case "RabbitMQ":
                            var rabbitmq = section.GetSection("RabbitMQ");
                            var host = rabbitmq.GetValue<string>("Host");
                            var virtualHost = rabbitmq.GetValue<string>("VirtualHost");
                            var username = rabbitmq.GetValue<string>("UserName");
                            var password = rabbitmq.GetValue<string>("Password");

                            services.AddMassTransit(masstransit =>
                            {
                                var assemblyName = configuration.GetValue<string>("AssemblyName");
                                var assembly = Assembly.Load(assemblyName!);

                                masstransit.AddConsumers(assembly);

                                masstransit.UsingRabbitMq((context, bus) =>
                                {
                                    bus.Host(host, virtualHost, host =>
                                    {
                                        host.Username(username!);
                                        host.Password(password!);
                                    });

                                    bus.ConfigureEndpoints(context);
                                });
                            });

                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
