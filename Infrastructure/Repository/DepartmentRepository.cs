namespace Infrastructure.Repository;

public class DepartmentRepository : GenericRepositoryAsync<Department>, IDepartmentRepository
{
    #region Fields

    private readonly DbSet<Department> _dBContext;

    #endregion

    #region Constructor

    public DepartmentRepository(ApplicationDBContext dBContext) : base(dBContext)
    {
        _dBContext = dBContext.Set<Department>();
    }

    #endregion


    #region Handles Functions

    #region Get List Of Department


    #endregion

    #endregion
}
