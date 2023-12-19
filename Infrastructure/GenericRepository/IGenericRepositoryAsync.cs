namespace Infrastructure.GenericRepository;

public interface IGenericRepositoryAsync<T> where T : class
{
    IDbContextTransaction BeginTransaction();
    void Commit();
    void RollBack();

    IQueryable<T> GetTableNoTracking();
    IQueryable<T> GetTableAsTracking();

    Task<T> GetByIdAsync(int id);
    
    Task<T> AddAsync(T entity);
    Task AddRangeAsync(ICollection<T> entities);
    
    Task UpdateAsync(T entity);
    Task UpdateRangeAsync(ICollection<T> entities);

    Task DeleteAsync(T entity);
    Task DeleteRangeAsync(ICollection<T> entities);
    
    Task SaveChangesAsync();
}
