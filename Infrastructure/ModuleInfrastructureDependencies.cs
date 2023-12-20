using Infrastructure.Persistence;
using System.Data;

namespace Infrastructure;

public static class ModuleInfrastructureDependencies
{
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
    {
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
        services.AddTransient<IStudentRepository, StudentRepository>();
        services.AddTransient<IDepartmentRepository, DepartmentRepository>();
        services.AddTransient<IInstructorRepository, InstructorRepository>();
        services.AddTransient<ISubjectsRepository, SubjectsRepository>();

        return services;
    }
}
