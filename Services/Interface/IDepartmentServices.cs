namespace Services.Interface;

public interface IDepartmentServices
{
    Task<Department> GetDepartmentByIdAsync(int id);
    IQueryable<Student> GetStudentByDepartmentIDAbleAsync(int DID);
}
