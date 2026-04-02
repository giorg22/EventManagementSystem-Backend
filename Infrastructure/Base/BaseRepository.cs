using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

public abstract class BaseRepository<T, TId> : IBaseRepository<T, TId> where T : BaseEntity<TId>
{
    private readonly ApplicationDbContext _dbContext;

    public BaseRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private DbSet<T> DbSet => _dbContext.Set<T>();

    public virtual async Task<T> AddAsync(T entity)
    {
        var entry = await DbSet.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entry.Entity;
    }

    public virtual async Task<T> UpdateAsync(T entity)
    {
        var entry = DbSet.Update(entity);
        await _dbContext.SaveChangesAsync();
        return entry.Entity;
    }

    public virtual async Task<int> CountAsync(Expression<Func<T, bool>> expression)
    {
        return await DbSet.CountAsync(expression);
    }

    public virtual async Task<int> CountAsync()
    {
        return await DbSet.CountAsync();
    }

    public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
    {
        return await DbSet.AnyAsync(expression);
    }

    public virtual async Task<T?> GetByIdAsync(TId id)
    {
        return await DbSet.FindAsync(id);
    }

    public virtual IQueryable<T> GetAll() => DbSet.AsNoTracking();

    public virtual async Task<T?> FirstOrDefaultAsync(Expression<Func<T?, bool>> expression)
    {
        return await DbSet.FirstOrDefaultAsync(expression);
    }

    public virtual async Task<List<T>> ListAsync(Expression<Func<T, bool>>? expression = default)
    {
        var predicate = expression ?? (x => true);
        return await DbSet.Where(predicate).ToListAsync();
    }

    public virtual async Task<int> DeleteAsync(T entity)
    {
        DbSet.Remove(entity);
        return await _dbContext.SaveChangesAsync();
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await DbSet.ToListAsync();
    }
}