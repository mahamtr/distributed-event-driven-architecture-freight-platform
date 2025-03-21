using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Contracts
{
    public static class MassTransitConfig
    {
        public static void AddMassTransitConfiguration(this IServiceCollection services, Action<IBusRegistrationConfigurator>? configure = null)
        {
            services.AddMassTransit(cfg =>
            {
                // Allow additional configuration (e.g., adding consumers)
                configure?.Invoke(cfg);

                // Configure RabbitMQ
                cfg.UsingRabbitMq((context, configurator) =>
                {
                    configurator.Host("rabbitmq", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    configurator.ConfigureEndpoints(context);
                });
            });
        }
    }
}