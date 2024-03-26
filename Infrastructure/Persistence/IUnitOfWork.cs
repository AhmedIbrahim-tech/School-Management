using Data.Entities.Models;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence;

public interface IUnitOfWork : IDisposable
{
    IGenericRepositoryAsync<Student> GenericStudentRepository { get; }
    IStudentRepository StudentRepository { get; }
    IDepartmentRepository DepartmentRepository {get;}
}


public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDBContext _dBContext;
    private readonly IConfiguration _configuration;
    private readonly IGenericRepositoryAsync<Student> _GenericStudentRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly IDepartmentRepository _departmentRepository;

    public UnitOfWork(
        ApplicationDBContext dBContext, 
        IConfiguration configuration, 
        IGenericRepositoryAsync<Student> GenericStudentRepository, 
        IStudentRepository studentRepository,
        IDepartmentRepository DepartmentRepository
    )
    {
        _dBContext = dBContext;
        _configuration = configuration;
        _GenericStudentRepository = GenericStudentRepository;
        _studentRepository = studentRepository;
        _departmentRepository = DepartmentRepository;
    }


    IGenericRepositoryAsync<Student> IUnitOfWork.GenericStudentRepository => _GenericStudentRepository ?? new GenericRepositoryAsync<Student>(_dBContext);
    IStudentRepository IUnitOfWork.StudentRepository => _studentRepository ?? new StudentRepository(_dBContext);
    IDepartmentRepository IUnitOfWork.DepartmentRepository => _departmentRepository ?? new DepartmentRepository(_dBContext);

    public void Dispose()
    {
        if (_dBContext != null)
        {
            _dBContext.Dispose();
        }

    }
}