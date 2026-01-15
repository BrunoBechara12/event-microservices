using Adapters.Secondary.Data.Context;
using Adapters.Secondary.Data.Repositories;
using Domain.Ports;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Adapters.Secondary.Data;
public static class ServiceInfraDataExtensions
{
    public static IServiceCollection AddDataBaseService(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<GuestDbContext>(options =>
            options.UseNpgsql(connectionString));
        services.AddScoped<IGuestRepository, GuestRepository>();
        return services;
    }
}
