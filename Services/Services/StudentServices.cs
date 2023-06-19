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

    #region Get Student By Id
    public async Task<Student> GetStudentsByIdAsync(int id)
    {
        var result = await _studentRepository.GetTableNoTracking()
                                       .Include(x => x.Department)
                                       .Where(x => x.StudID.Equals(id))
                                       .FirstOrDefaultAsync();
        return result;
    } 
    #endregion

    #region Add Student
    public async Task<string> AddAsync(Student student)
    {
        // Check if Student is Exist
        var CheckExist = await _studentRepository.GetTableNoTracking().Where(x=>x.Name.Equals(student.Name)).FirstOrDefaultAsync();
        if (CheckExist != null)
        {
            return "Exist";
        }
        // Add Student
        else
        {
            await _studentRepository.AddAsync(student);
            return "Successfully";
        }
    } 
    #endregion

    #endregion
}
