namespace API.Configurations;

public static class DatabaseConfiguration
{
    public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDBContext>(option =>
            option.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDBContext).Assembly.FullName)));
    }
}
