namespace Infrastructure.Repository;

public class StudentRepository : IStudentRepository
{
    #region Fields

    private readonly ApplicationDBContext _dBContext;

    #endregion

    #region Constructor

    public StudentRepository(ApplicationDBContext dBContext)
    {
        _dBContext = dBContext;
    }

    #endregion


    #region Handles Functions

    #region Get List Of Students

    public async Task<List<Student>> GetStudentsListAsync()
    {
        return await _dBContext.students.Include(x=>x.Department).ToListAsync();
    }

    #endregion

    #endregion
}
