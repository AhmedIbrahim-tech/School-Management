namespace Services.Services;

public class StudentServices : IStudentServices
{
    #region Fields

    private readonly IStudentRepository _studentRepository;

    #endregion

    #region Constructor

    public StudentServices(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    #endregion


    #region Handles Functions

    #region Get List Of Students

    public async Task<List<Student>> GetStudentsListAsync()
    {
        return await _studentRepository.GetStudentsListAsync();
    }

    #endregion

    #endregion
}
