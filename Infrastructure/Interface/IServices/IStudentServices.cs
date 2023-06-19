namespace Infrastructure.Interface.IServices;

public interface IStudentServices
{
    Task<List<Student>> GetStudentsListAsync();
}
