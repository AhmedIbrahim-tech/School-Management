
namespace Infrastructure.Interface.IRepository;

public interface IStudentRepository : IGenericRepositoryAsync<Student>
{
    Task<List<Student>> GetStudentsListAsync();
}
