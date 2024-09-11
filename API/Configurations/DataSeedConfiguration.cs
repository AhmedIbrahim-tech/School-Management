using Data.Entities.Identities;
using Infrastructure.Context.DataSeed;
using Microsoft.AspNetCore.Identity;

namespace API.Configurations;

public static class DataSeedConfiguration
{
    public static async Task SeedData(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
            await RoleSeeder.SeedAsync(roleManager);
            await UserSeeder.SeedAsync(userManager);
        }
    }

    public static async Task UpdateDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<ApplicationDBContext>();
        var logger = services.GetRequiredService<ILogger<Program>>();

        try
        {
            await context.Database.MigrateAsync();
            //await StoreContextSeed.SeedAsync(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error Occurred While Migrating Process");
        }
    }
}
