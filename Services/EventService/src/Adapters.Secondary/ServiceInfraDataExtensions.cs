using Domain.Ports.Output;
using Adapters.Secondary.Context;
using Adapters.Secondary.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Adapters.Secondary;
public static class ServiceInfraDataExtensions
{
    public static IServiceCollection AddDataBaseService(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<EventDbContext>(options =>
            options.UseNpgsql(connectionString));
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<ICollaboratorRepository, CollaboratorRepository>();
        return services;
    }
}
