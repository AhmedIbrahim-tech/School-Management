namespace Infrastructure.Repository;

public class SubjectsRepository : GenericRepositoryAsync<Subject>, ISubjectsRepository
{
    #region Fields

    private readonly DbSet<Subject> _dBContext;

    #endregion

    #region Constructor

    public SubjectsRepository(ApplicationDBContext dBContext) : base(dBContext)
    {
        _dBContext = dBContext.Set<Subject>();
    }

    #endregion


    #region Handles Functions

    #region Get List Of Subjects


    #endregion

    #endregion
}
