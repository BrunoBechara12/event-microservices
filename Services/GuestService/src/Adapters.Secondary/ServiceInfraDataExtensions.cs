using Adapters.Secondary.Context;
using Adapters.Secondary.Messaging;
using Adapters.Secondary.Repositories;
using Domain.Ports.Output;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Adapters.Secondary;
public static class ServiceInfraDataExtensions
{
    public static IServiceCollection AddDataBaseService(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<GuestDbContext>(options =>
            options.UseNpgsql(connectionString));
        services.AddScoped<IGuestRepository, GuestRepository>();
        services.AddScoped<IEventRepository, EventRepository>();
        return services;
    }

    public static void AddRabbitMQService(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitMQSection = configuration.GetSection("RabbitMQ");

        services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.AddConsumer<EventCreatedConsumer>();
            busConfigurator.AddConsumer<EventDeletedConsumer>();

            busConfigurator.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(new Uri(rabbitMQSection["Host"]!), host =>
                {
                    host.Username(rabbitMQSection["Username"]!);
                    host.Password(rabbitMQSection["Password"]!);
                });

                cfg.ConfigureEndpoints(ctx);
            });
        });
    }
}
