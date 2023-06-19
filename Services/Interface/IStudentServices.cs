namespace Services.Interface.IServices;

public interface IStudentServices
{
    Task<List<Student>> GetStudentsListAsync();
    Task<Student> GetStudentsByIdAsync(int id);
}
