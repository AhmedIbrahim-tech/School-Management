namespace Infrastructure.GenericRepository;

public interface IGenericRepositoryAsync<T> where T : class
{
    IDbContextTransaction BeginTransaction();
    void Commit();
    void RollBack();

    IQueryable<T> GetTableNoTracking();  // Used when only data needs to be read without having to track or modify that data, which improves performance.
    IQueryable<T> GetTableAsTracking();  // sed when you need to track entities and update them later in the database.

    Task<T> GetByIdAsync(int id);
    
    Task<T> AddAsync(T entity);
    Task AddRangeAsync(ICollection<T> entities);
    
    Task UpdateAsync(T entity);
    Task UpdateRangeAsync(ICollection<T> entities);

    Task DeleteAsync(T entity);
    Task DeleteRangeAsync(ICollection<T> entities);
    
    Task SaveChangesAsync();
}
