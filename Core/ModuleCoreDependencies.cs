using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Core;

public static class ModuleCoreDependencies
{
    public static IServiceCollection AddCoreDependencies(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        return services;
    }
}
