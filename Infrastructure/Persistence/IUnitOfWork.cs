using Data.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence;

public interface IUnitOfWork : IDisposable
{
    void BeginTransaction();
    void Commit();
    void Rollback();
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();

    IGenericRepositoryAsync<Student> GenericStudentRepository { get; }
    IStudentRepository StudentRepository { get; }
    IDepartmentRepository DepartmentRepository { get; }
}

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDBContext _context;
    private readonly IConfiguration _configuration;
    private IGenericRepositoryAsync<Student> _genericStudentRepository;
    private IStudentRepository _studentRepository;
    private IDepartmentRepository _departmentRepository;
    private IDbContextTransaction _transaction;

    public UnitOfWork(
        ApplicationDBContext context,
        IConfiguration configuration,
        IGenericRepositoryAsync<Student> genericStudentRepository,
        IStudentRepository studentRepository,
        IDepartmentRepository departmentRepository)
    {
        _context = context;
        _configuration = configuration;
        _genericStudentRepository = genericStudentRepository;
        _studentRepository = studentRepository;
        _departmentRepository = departmentRepository;
    }

    public void BeginTransaction()
    {
        _transaction = _context.Database.BeginTransaction();
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public void Commit()
    {
        if (_transaction != null)
        {
            _transaction.Commit();
        }
    }

    public async Task CommitAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
        }
    }

    public void Rollback()
    {
        if (_transaction != null)
        {
            _transaction.Rollback();
        }
    }

    public async Task RollbackAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
        }
    }

    public IGenericRepositoryAsync<Student> GenericStudentRepository =>
        _genericStudentRepository ??= new GenericRepositoryAsync<Student>(_context);

    public IStudentRepository StudentRepository =>
        _studentRepository ??= new StudentRepository(_context);

    public IDepartmentRepository DepartmentRepository =>
        _departmentRepository ??= new DepartmentRepository(_context);

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}
