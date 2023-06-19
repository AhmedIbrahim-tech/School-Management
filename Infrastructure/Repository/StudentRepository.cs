using Infrastructure.Interface;

namespace Infrastructure.Repository;

public class StudentRepository : GenericRepositoryAsync<Student>, IStudentRepository
{
    #region Fields

    private readonly DbSet<Student> _dBContext;

    #endregion

    #region Constructor

    public StudentRepository(ApplicationDBContext dBContext) : base(dBContext)
    {
        _dBContext = dBContext.Set<Student>();
    }

    #endregion


    #region Handles Functions

    #region Get List Of Students

    public async Task<List<Student>> GetStudentsListAsync()
    {
        return await _dBContext.Include(x => x.Department).ToListAsync();
    }

    #endregion

    #endregion
}
