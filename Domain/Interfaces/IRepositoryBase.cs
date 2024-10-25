using Scheduler.Domain.Entities;

namespace Scheduler.Domain.Interfaces;

public interface IRepositoryBase<T> where T : BaseEntity
{
    Task<IEnumerable<T?>> GetAllAsync();
    Task<IEnumerable<T?>> GetAllActiveAsync();
    Task<T?> GetByIdAsync(Guid Uuid);
    Task<T?> GetActiveByIdAsync(Guid Uuid);
    Task<T?> CreateAsync(T entity);
    Task<T?> UpdateAsync(T entity);
    Task<T?> DeleteAsync(T entity);
    Task<T?> DeleteAsync(Guid Uuidd);
    Task<T?> SoftDeleteAsync(T entity);
    Task<T?> SoftDeleteAsync(Guid Uuid);
}
