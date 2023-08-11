namespace Services.Interface;

public interface IDepartmentServices
{
    Task<Department> GetDepartmentByIdAsync(int id);
    public Task<bool> IsDepartmentIdExist(int departmentId);
}
