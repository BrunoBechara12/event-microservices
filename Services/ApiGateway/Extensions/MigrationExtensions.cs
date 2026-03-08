using ApiGateway.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiGateway.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GatewayDbContext>();
        dbContext.Database.Migrate();
    }
}
