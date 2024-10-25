using Scheduler.Application.DTOs.ApiKey;

namespace Scheduler.Application.Services.Interfaces;

public interface IApiKeyService
{
    Task<IEnumerable<ApiKeyDto?>> GetAllApiKeyAsync();
    Task<ApiKeyDto?> GetApiKeyOrNullAsync(Guid uuid);
    Task<ApiKeyDto> CreateApiKeyAsync(CreateApiKeyDto createApiKeyDto);
    Task<ApiKeyDto?> UpdateApiKeyAsync(UpdateApiKeyDto updateApiKeyDto, Guid uuid);
    Task<bool> DeleteApiKeyAsync(Guid uuid);
    Task<ApiKeyDto?> GetByTokenAsync(string apiKey);
}
