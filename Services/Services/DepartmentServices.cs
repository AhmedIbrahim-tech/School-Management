using Infrastructure.Persistence;
using Services.Interface;

namespace Services.Services;

public class DepartmentServices : IDepartmentServices
{
    #region Fields
    private readonly IUnitOfWork _unitOfWork;
    #endregion

    #region Constructor (s)
    public DepartmentServices(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    #endregion

    #region Handle Functions
    public async Task<Department> GetDepartmentByIdAsync(int id)
    {
        var result = await _unitOfWork.DepartmentRepository.GetTableNoTracking()
                                                     .Where(x => x.DID.Equals(id))
                                                     .Include(x => x.DepartmentSubjects).ThenInclude(x => x.Subject)
                                                     .Include(x => x.Instructors)
                                                     .Include(x => x.Instructor)
                                                     .Include(x => x.Students)
                                                     .FirstOrDefaultAsync();
        return result ?? new Department();
    }



    public IQueryable<Student> GetStudentByDepartmentIDAbleAsync(int DID)
    {
        var Queryable = _unitOfWork.StudentRepository.GetTableNoTracking().Where(x => x.DID.Equals(DID)).AsQueryable();
        return Queryable;
    }

    #endregion
}
