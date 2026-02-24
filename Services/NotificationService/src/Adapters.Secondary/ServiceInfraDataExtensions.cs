using Adapters.Secondary.Context;
using Adapters.Secondary.Messaging;
using Adapters.Secondary.Repositories;
using Adapters.Secondary.WhatsApp;
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
        var connectionString = configuration.GetConnectionString("NotificationDb");

        services.AddDbContext<NotificationDbContext>(options =>
            options.UseNpgsql(connectionString));
        
        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<IMessageTemplateRepository, MessageTemplateRepository>();
        
        services.AddHttpClient<IWhatsAppService, EvolutionApiService>(client =>
        {
            var baseUrl = configuration["EvolutionApi:BaseUrl"] ?? "http://localhost:8080";
            var apiKey = configuration["EvolutionApi:ApiKey"];

            client.BaseAddress = new Uri(baseUrl);
            
            if (!string.IsNullOrEmpty(apiKey))
            {
                client.DefaultRequestHeaders.Add("apikey", apiKey);
            }
        });
        
        return services;
    }

    public static void AddRabbitMQService(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitMQSection = configuration.GetSection("RabbitMQ");

        services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.AddConsumer<GuestInvitedConsumer>();
            busConfigurator.AddConsumer<GuestConfirmedConsumer>();

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
