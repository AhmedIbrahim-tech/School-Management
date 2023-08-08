using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence;

public interface IUnitOfWork : IDisposable
{
    IGenericRepositoryAsync<Student> GenericStudentRepository { get; }
    IStudentRepository StudentRepository { get; }
}


public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDBContext _dBContext;
    private readonly IConfiguration _configuration;
    private readonly IGenericRepositoryAsync<Student> _GenericStudentRepository;
    private readonly IStudentRepository _studentRepository;

    public UnitOfWork(ApplicationDBContext dBContext, IConfiguration configuration, IGenericRepositoryAsync<Student> GenericStudentRepository, IStudentRepository studentRepository)
    {
        _dBContext = dBContext;
        _configuration = configuration;
        _GenericStudentRepository = GenericStudentRepository;
        _studentRepository = studentRepository;
    }


    IGenericRepositoryAsync<Student> IUnitOfWork.GenericStudentRepository => _GenericStudentRepository ?? new GenericRepositoryAsync<Student>(_dBContext);
    IStudentRepository IUnitOfWork.StudentRepository => _studentRepository ?? new StudentRepository(_dBContext);

    public void Dispose()
    {
        if (_dBContext != null)
        {
            _dBContext.Dispose();
        }

    }
}