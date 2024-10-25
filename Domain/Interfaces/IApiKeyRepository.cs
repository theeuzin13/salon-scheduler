using Scheduler.Domain.Entities;

namespace Scheduler.Domain.Interfaces;

public interface IApiKeyRepository
{
    Task<ApiKey?> GetByIdAsync(Guid uuid);
    Task<IEnumerable<ApiKey?>> GetAllAsync();
    Task<ApiKey?> CreateAsync(ApiKey apiKey);
    Task<ApiKey?> UpdateAsync(ApiKey apiKey);
    Task<bool> DeleteAsync(Guid uuid);
    Task<ApiKey?> GetByTokenAsync(string apiKey);
}
