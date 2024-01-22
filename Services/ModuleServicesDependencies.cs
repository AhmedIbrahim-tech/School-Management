using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Services.Interface;
using Services.Services;

namespace Services;

public static class ModuleServicesDependencies
{
    public static IServiceCollection AddServicesDependencies(this IServiceCollection services)
    {
        services.AddTransient<IStudentServices, StudentServices>();
        services.AddTransient<IDepartmentServices, DepartmentServices>();
        services.AddTransient<IApplicationUserService, ApplicationUserService>();
        services.AddTransient<IAuthenticationService, AuthenticationService>();

        return services;
    }
}
