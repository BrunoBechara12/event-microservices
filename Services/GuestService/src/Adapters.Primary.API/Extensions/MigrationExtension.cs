using Adapters.Secondary.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Adapters.Primary.API.Extensions;

public static class MigrationExtension
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using GuestDbContext dbContext =
            scope.ServiceProvider.GetRequiredService<GuestDbContext>();

        dbContext.Database.Migrate();
    }
}
