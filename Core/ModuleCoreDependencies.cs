using Core.Behavior;

namespace Core;

public static class ModuleCoreDependencies
{
    public static IServiceCollection AddCoreDependencies(this IServiceCollection services)
    {
        //Configuration of IMediator
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        //Configuration of AutoMapper
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        //Add Fluent Validation
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // ValidationBehavior
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}
