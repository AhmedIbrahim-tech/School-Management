using Microsoft.Extensions.DependencyInjection;
using Services.AuthServices.Implementations;
using Services.AuthServices.Interfaces;
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
        services.AddTransient<IAuthorizationService, AuthorizationService>();
        
        //services.AddTransient<IEmailsService, EmailsService>();
        services.AddTransient<ICurrentUserService, CurrentUserService>();
        //services.AddTransient<IInstructorService, InstructorService>();
        services.AddTransient<IFileService, FileService>();
        return services;
    }
}
