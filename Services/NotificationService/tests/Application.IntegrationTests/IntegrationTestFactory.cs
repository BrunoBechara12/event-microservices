using Adapters.Secondary.Context;
using Domain.Ports.Output;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Testcontainers.PostgreSql;
using Testcontainers.RabbitMq;
using Xunit;

namespace Application.IntegrationTests;

public class IntegrationTestFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgreSqlContainer = new PostgreSqlBuilder()
        .WithDatabase("notifications")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .Build();

    private readonly RabbitMqContainer _rabbitMqContainer = new RabbitMqBuilder()
        .WithUsername("guest")
        .WithPassword("guest")
        .Build();

    public string RabbitMqHost => _rabbitMqContainer.Hostname;
    public int RabbitMqPort => _rabbitMqContainer.GetMappedPublicPort(5672);
    public Mock<IWhatsAppService> WhatsAppServiceMock { get; } = new();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<NotificationDbContext>));
            services.RemoveAll(typeof(NotificationDbContext));

            services.AddDbContext<NotificationDbContext>(options =>
                options.UseNpgsql(_postgreSqlContainer.GetConnectionString()));

            services.RemoveAll(typeof(IWhatsAppService));
            services.AddSingleton(WhatsAppServiceMock.Object);

            RemoveMassTransitServices(services);

            services.AddMassTransit(busConfigurator =>
            {
                busConfigurator.AddConsumers(typeof(Adapters.Secondary.Messaging.GuestInvitedConsumer).Assembly);

                busConfigurator.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(RabbitMqHost, (ushort)RabbitMqPort, "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });
        });
    }

    private static void RemoveMassTransitServices(IServiceCollection services)
    {
        var massTransitTypes = services
            .Where(s => s.ServiceType.FullName?.Contains("MassTransit") == true ||
                       s.ImplementationType?.FullName?.Contains("MassTransit") == true)
            .ToList();

        foreach (var service in massTransitTypes)
        {
            services.Remove(service);
        }

        services.RemoveAll(typeof(IBus));
        services.RemoveAll(typeof(IBusControl));
        services.RemoveAll(typeof(IPublishEndpoint));
    }

    public async Task InitializeAsync()
    {
        await _postgreSqlContainer.StartAsync();
        await _rabbitMqContainer.StartAsync();

        WhatsAppServiceMock
            .Setup(x => x.SendTextMessageAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new WhatsAppSendResult(true, "mock-message-id"));

        WhatsAppServiceMock
            .Setup(x => x.IsConnectedAsync())
            .ReturnsAsync(true);
    }

    public new async Task DisposeAsync()
    {
        await _postgreSqlContainer.DisposeAsync();
        await _rabbitMqContainer.DisposeAsync();
    }

    public async Task ResetDatabaseAsync()
    {
        using var scope = Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<NotificationDbContext>();
        
        await context.Database.EnsureDeletedAsync();
        await context.Database.MigrateAsync();
    }
}
