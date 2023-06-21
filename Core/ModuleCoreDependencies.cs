using FluentValidation.AspNetCore;

namespace Core;

public static class ModuleCoreDependencies
{
    public static IServiceCollection AddCoreDependencies(this IServiceCollection services)
    {
        //Congigration of IMediator
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        //Configration of AutoMapper
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        //Add Fluent Validation
        services.AddFluentValidation(options =>
        {
            options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
        });
        return services;
    }
}
