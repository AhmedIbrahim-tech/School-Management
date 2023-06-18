
namespace Data.Interface.IRepository;

public interface IStudentRepository
{
    Task<List<Student>> GetStudentsListAsync();
}
