using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BuildingBlocks.Messaging.MassTransit;

public static class Extensions
{
    public static IServiceCollection AddMessageBroker(this IServiceCollection services, 
        IConfiguration configuration,
        Assembly? assembly = null)
    {
        services.AddMassTransit(mtConfig =>
        {
            mtConfig.SetKebabCaseEndpointNameFormatter();

            if (assembly is not null)
            {
                mtConfig.AddConsumers(assembly);
            }
            
            mtConfig.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(new Uri(configuration["MessageBroker:RabbitMQ:Host"]!), host =>
                {
                    host.Username(configuration["MessageBroker:RabbitMQ:Username"]!);
                    host.Password(configuration["MessageBroker:RabbitMQ:Password"]!);
                });
                cfg.ConfigureEndpoints(context);
            });
        });
        return services;
    }
}