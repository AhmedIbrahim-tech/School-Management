using Microsoft.Extensions.DependencyInjection;
using Services.Services;

namespace Services;

public static class ModuleServicesDependencies
{
    public static IServiceCollection AddServicesDependencies(this IServiceCollection services)
    {
        services.AddTransient<IStudentServices, StudentServices>();
        return services;
    }
}
