using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Data;
public static class ServiceInfraDataExtensions
{
    public static IServiceCollection AddDataBaseService(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<EventDbContext>(options =>
            options.UseSqlServer(connectionString));

        return services;
    }
}
