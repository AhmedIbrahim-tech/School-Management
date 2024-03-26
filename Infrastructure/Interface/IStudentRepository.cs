using Data.Entities.Models;

namespace Infrastructure.Interface;

public interface IStudentRepository : IGenericRepositoryAsync<Student>
{
    Task<List<Student>> GetStudentsListAsync();
}
