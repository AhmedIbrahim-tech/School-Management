using Data.Entities.Models;
using Infrastructure.Persistence;

namespace Services.Services;

public class StudentServices : IStudentServices
{
    #region Fields

    private readonly IStudentRepository _studentRepository;
    private readonly IUnitOfWork _unitOfWork;

    #endregion

    #region Constructor
    public StudentServices(IStudentRepository studentRepository, IUnitOfWork unitOfWork)
    {
        _studentRepository = studentRepository;
        this._unitOfWork = unitOfWork;
    }
    #endregion


    #region Handles Functions

    #region Get List Of Students

    public async Task<List<Student>> GetStudentsListAsync()
    {
        //return await _studentRepository.GetStudentsListAsync();
        return await _unitOfWork.StudentRepository.GetStudentsListAsync();
    }

    #endregion

    #region IQueryable of Students
    public IQueryable<Student> GetStudentsQueryAbleAsync()
    {
        return _studentRepository.GetTableNoTracking().Include(x => x.Department).AsQueryable();
    }

    public IQueryable<Student> FilterStudentsPaginationQueryAbleAsync(StudentOrderEnum orderEnum, string? search)
    {
        var Queryable = _studentRepository.GetTableNoTracking().Include(x => x.Department).AsQueryable();
        if (Queryable != null)
        {
            Queryable.Where(x => x.NameEn.Contains(search) || x.Address.Contains(search));
        }
        switch (orderEnum)
        {
            case StudentOrderEnum.StudID:
                Queryable = Queryable.OrderBy(x => x.StudID);
                break;

            case StudentOrderEnum.Name:
                Queryable = Queryable.OrderBy(x => x.NameEn);
                break;

            case StudentOrderEnum.Address:
                Queryable = Queryable.OrderBy(x => x.Address);
                break;

            case StudentOrderEnum.DepartmentName:
                Queryable = Queryable.OrderBy(x => x.Department.DNameEn);
                break;

            default:
                Queryable = Queryable.OrderBy(x => x.StudID);
                break;


        }
        return Queryable;
    }
    #endregion

    #region Get Student By Id
    public async Task<Student> GetStudentsByIdAsync(int id)
    {
        //var result = await _studentRepository.GetTableNoTracking()
        //                               .Include(x => x.Department)
        //                               .Where(x => x.StudID.Equals(id))
        //                               .FirstOrDefaultAsync();
        var result = await _unitOfWork.StudentRepository
            .GetTableNoTracking().Include(x => x.Department).Where(x => x.StudID.Equals(id)).FirstOrDefaultAsync();
        return result;
    }
    #endregion

    #region Add Student
    public async Task<int> AddAsync(Student student)
    {
        #region Old Function 
        //// Check if Student is Exist
        //var CheckExist = await _studentRepository.GetTableNoTracking().Where(x => x.Name.Equals(student.Name)).FirstOrDefaultAsync();
        //if (CheckExist != null)
        //{
        //    return "Exist";
        //}
        //// Add Student
        //else
        //{
        //    await _studentRepository.AddAsync(student);
        //    return "Successfully";
        //}
        #endregion
        var result =  await _studentRepository.AddAsync(student);
        return result.StudID;
    }
    #endregion

    #region Check Name Exist
    public async Task<bool> IsExistNameAsync(string name)
    {
        var response = await _studentRepository.GetTableNoTracking().Where(x => x.NameEn.Equals(name)).FirstOrDefaultAsync();
        if (response == null) return false;
        return true;
    }

    public async Task<bool> IsExistNameExcuteSelfAsync(string name, int id)
    {
        var response = await _studentRepository.GetTableNoTracking().Where(x => x.NameEn.Equals(name) & !x.StudID.Equals(id)).FirstOrDefaultAsync();
        if (response == null) return false;
        return true;
    }

    #endregion

    #region Edit Student
    public async Task<int> EditAsync(Student student)
    {
        await _studentRepository.UpdateAsync(student);
        return 1;
    }
    #endregion

    #region Delete Student
    public async Task<int> DeleteAsync(Student student)
    {
        await _studentRepository.DeleteAsync(student);
        return 1;
    }
    #endregion

    public IQueryable<Student> GetStudentsByDepartmentIDQuerable(int DID)
    {
        var Queryable = _unitOfWork.StudentRepository.GetTableNoTracking().Where(x => x.DID.Equals(DID)).AsQueryable();
        return Queryable;
    }

    #endregion
}
