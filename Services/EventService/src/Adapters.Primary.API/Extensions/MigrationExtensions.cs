using Adapters.Secondary.Context;
using Microsoft.EntityFrameworkCore;

namespace Adapters.Primary.API.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using EventDbContext dbContext =
            scope.ServiceProvider.GetRequiredService<EventDbContext>();

        dbContext.Database.Migrate();
    }
}
