using Adapters.Secondary.Context;
using Adapters.Secondary.MessageHandler;
using Domain.Ports.Output;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using Respawn;
using System.Data.Common;
using Testcontainers.PostgreSql;
using Testcontainers.RabbitMq;

namespace Application.IntegrationTests;

public class IntegrationTestFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private Respawner _respawner = default!;
    private DbConnection _connection = default!;

    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:15-alpine")
        .WithDatabase("events")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .Build();

    private readonly RabbitMqContainer _rabbitMqContainer = new RabbitMqBuilder()
        .WithImage("rabbitmq:3-management-alpine")
        .WithUsername("guest")
        .WithPassword("guest")
        .Build();

    public string RabbitMqHost => _rabbitMqContainer.Hostname;
    public int RabbitMqPort => _rabbitMqContainer.GetMappedPublicPort(5672);

    public async Task ResetDatabaseAsync()
    {
        await _respawner.ResetAsync(_connection);
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var dbContextDescriptor = services
                .SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<EventDbContext>));

            if (dbContextDescriptor is not null)
            {
                services.Remove(dbContextDescriptor);
            }

            services.AddDbContext<EventDbContext>(options =>
            {
                options.UseNpgsql(_dbContainer.GetConnectionString());
            });

            RemoveMassTransitServices(services);

            services.AddMassTransit(busConfigurator =>
            {
                busConfigurator.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(RabbitMqHost, (ushort)RabbitMqPort, "/", host =>
                    {
                        host.Username("guest");
                        host.Password("guest");
                    });

                    cfg.ConfigureEndpoints(ctx);
                });
            });

            services.AddScoped<IMessagePublisher, MassTransitPublisher>();
        });
    }

    private static void RemoveMassTransitServices(IServiceCollection services)
    {
        var massTransitDescriptors = services
            .Where(d => d.ServiceType.FullName?.Contains("MassTransit") == true ||
                       d.ImplementationType?.FullName?.Contains("MassTransit") == true)
            .ToList();

        foreach (var descriptor in massTransitDescriptors)
        {
            services.Remove(descriptor);
        }

        services.RemoveAll<IBus>();
        services.RemoveAll<IBusControl>();
        services.RemoveAll<IPublishEndpoint>();
        services.RemoveAll<ISendEndpointProvider>();
        services.RemoveAll<IMessagePublisher>();
    }

    public async Task InitializeAsync()
    {
        await Task.WhenAll(
            _dbContainer.StartAsync(),
            _rabbitMqContainer.StartAsync()
        );

        using (var scope = Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<EventDbContext>();
            await dbContext.Database.MigrateAsync();
        }

        _connection = new NpgsqlConnection(_dbContainer.GetConnectionString());
        await _connection.OpenAsync();

        _respawner = await Respawner.CreateAsync(_connection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres,
            SchemasToInclude = new[] { "public" }
        });
    }

    public new async Task DisposeAsync()
    {
        if (_connection != null)
        {
            await _connection.CloseAsync();
        }
        
        await Task.WhenAll(
            _dbContainer.StopAsync(),
            _rabbitMqContainer.StopAsync()
        );
    }
}
