using System.Linq.Expressions;

public interface IBaseRepository<T, TId> where T : BaseEntity<TId>
{
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);

    Task<int> CountAsync(Expression<Func<T, bool>> expression);

    Task<int> CountAsync();

    Task<List<T>> GetAllAsync();

    Task<bool> AnyAsync(Expression<Func<T, bool>> expression);

    IQueryable<T> GetAll();
    Task<T?> GetByIdAsync(TId id);
    Task<T?> FirstOrDefaultAsync(Expression<Func<T?, bool>> expression);

    Task<List<T>> ListAsync(Expression<Func<T, bool>>? expression = default);

    Task<int> DeleteAsync(T entity);
}