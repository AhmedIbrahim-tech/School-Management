namespace Services.Interface.IServices;

public interface IStudentServices
{
    Task<List<Student>> GetStudentsListAsync();
    IQueryable<Student> GetStudentsQueryAbleAsync();
    IQueryable<Student> FilterStudentsPaginationQueryAbleAsync(StudentOrderEnum orderEnum, string search);
    Task<Student> GetStudentsByIdAsync(int id);
    Task<int> AddAsync(Student student);
    Task<bool> IsExistNameAsync(string name);
    Task<bool> IsExistNameExcuteSelfAsync(string name, int id);
    Task<int> EditAsync(Student student);
    Task<int> DeleteAsync(Student student);

    IQueryable<Student> GetStudentsByDepartmentIDQuerable(int DID);


}
