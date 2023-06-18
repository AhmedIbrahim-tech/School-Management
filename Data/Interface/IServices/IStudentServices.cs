namespace Data.Interface.IServices;

public interface IStudentServices
{
    Task<List<Student>> GetStudentsListAsync();
}
