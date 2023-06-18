using Microsoft.Extensions.Configuration;

namespace Infrastructure;

public static class ModuleInfrastructureDependencies
{
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
    {
        services.AddTransient<IStudentRepository, StudentRepository>();
        return services;
    }
}
