namespace Infrastructure.Repository;

public class InstructorRepository : GenericRepositoryAsync<Instructor>, IInstructorRepository
{
    #region Fields

    private readonly DbSet<Instructor> _dBContext;

    #endregion

    #region Constructor

    public InstructorRepository(ApplicationDBContext dBContext) : base(dBContext)
    {
        _dBContext = dBContext.Set<Instructor>();
    }

    #endregion


    #region Handles Functions

    #region Get List Of Instructor


    #endregion

    #endregion
}