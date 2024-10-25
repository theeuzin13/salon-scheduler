using Microsoft.EntityFrameworkCore;
using Scheduler.Domain.Entities;
using Scheduler.Domain.Interfaces;
using Scheduler.Infrastructure;


namespace Scheduler.Infrastructure.Repositories;


public class RepositoryBase<T> : IRepositoryBase<T> where T : BaseEntity
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public RepositoryBase(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<IEnumerable<T?>> GetAllAsync()
    {
        try
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while getting all {typeof(T).Name}.", ex);
        }
    }

    public async Task<IEnumerable<T?>> GetAllActiveAsync()
    {
        try
        {
            return await _dbSet.AsNoTracking().Where(x => x.DeletedAt == null).ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while getting all active {typeof(T).Name}.", ex);
        }
    }

    public async Task<T?> GetByIdAsync(Guid Uuid)
    {
        try
        {
            return await _dbSet.FindAsync(Uuid);
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while getting {typeof(T).Name} by id.", ex);
        }
    }

    public async Task<T?> GetActiveByIdAsync(Guid Uuid)
    {
        try
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Uuid == Uuid && x.DeletedAt == null);
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while getting active {typeof(T).Name} by id.", ex);
        }
    }

    public async Task<T?> CreateAsync(T entity)
    {
        try
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while creating {typeof(T).Name}.", ex);
        }
    }

    public async Task<T?> UpdateAsync(T entity)
    {
        try
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while updating {typeof(T).Name}.", ex);
        }
    }

    public async Task<T?> DeleteAsync(T entity)
    {
        try
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while deleting {typeof(T).Name}.", ex);
        }
    }

    public async Task<T?> DeleteAsync(Guid Uuid)
    {
        try
        {
            var entity = await _dbSet.FindAsync(Uuid);

            if (entity == null)
                return null;

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while deleting {typeof(T).Name}.", ex);
        }
    }

    public async Task<T?> SoftDeleteAsync(T entity)
    {
        try
        {
            entity.DeletedAt = DateTime.UtcNow;
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while soft deleting {typeof(T).Name}.", ex);
        }
    }

    public async Task<T?> SoftDeleteAsync(Guid Uuid)
    {
        try
        {
            var entity = await _dbSet.FindAsync(Uuid);

            if (entity == null)
                return null;

            entity.DeletedAt = DateTime.UtcNow;
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while soft deleting {typeof(T).Name}.", ex);
        }
    }
}
