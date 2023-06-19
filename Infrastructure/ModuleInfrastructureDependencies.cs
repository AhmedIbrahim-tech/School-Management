namespace Infrastructure;

public static class ModuleInfrastructureDependencies
{
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
    {
        services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
        services.AddTransient<IStudentRepository, StudentRepository>();

        return services;
    }
}
