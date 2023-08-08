namespace Infrastructure.Repository;

public class SubjectsRepository : GenericRepositoryAsync<Subjects>, ISubjectsRepository
{
    #region Fields

    private readonly DbSet<Subjects> _dBContext;

    #endregion

    #region Constructor

    public SubjectsRepository(ApplicationDBContext dBContext) : base(dBContext)
    {
        _dBContext = dBContext.Set<Subjects>();
    }

    #endregion


    #region Handles Functions

    #region Get List Of Subjects


    #endregion

    #endregion
}
