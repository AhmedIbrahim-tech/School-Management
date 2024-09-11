namespace API.Configurations;

public static class DependencyInjectionConfiguration
{
    public static void ConfigureDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructureDependencies()
            .AddServicesDependencies()
            .AddCoreDependencies()
            .AddServiceRegisteration(configuration);
    }
}