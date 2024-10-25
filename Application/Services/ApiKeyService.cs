using Scheduler.Application.DTOs.ApiKey;
using Scheduler.Application.Mappers;
using Scheduler.Application.Services.Interfaces;
using Scheduler.Domain.Entities;
using Scheduler.Domain.Interfaces;

namespace Scheduler.Application.Services;

public class ApiKeyService(IApiKeyRepository apiKeyRepository, ILogger<ApiKeyService> logger) : IApiKeyService
{
    private readonly IApiKeyRepository _apiKeyRepository = apiKeyRepository;
    private readonly ILogger<ApiKeyService> _logger = logger;

    public async Task<IEnumerable<ApiKeyDto?>> GetAllApiKeyAsync()
    {
        try
        {
            IEnumerable<ApiKey?> apiKeys = await _apiKeyRepository.GetAllAsync();
            return apiKeys.Select(apiKey => apiKey != null ? ApiKeyMapper.MapToDto(apiKey) : null);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving  all API keys");
            throw;
        }
    }

    public async Task<ApiKeyDto?> GetApiKeyOrNullAsync(Guid uuid)
    {
        try
        {
            ApiKey? apiKey = await _apiKeyRepository.GetByIdAsync(uuid);

            return apiKey is not null ? ApiKeyMapper.MapToDto(apiKey) : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving API key with UUID {UUID}.", uuid);
            throw;
        }

    }

    public async Task<ApiKeyDto> CreateApiKeyAsync(CreateApiKeyDto createApiKeyDto)
    {
        try
        {
            var apiKeyExists = await _apiKeyRepository.GetByTokenAsync(createApiKeyDto.Token);

            if (apiKeyExists != null)
            {
                throw new ArgumentException("Invalid Request");
            }
;
            var apiKey = ApiKeyMapper.MapToEntity(createApiKeyDto);
            var createdApiKey = await _apiKeyRepository.CreateAsync(apiKey);
            return ApiKeyMapper.MapToDto(createdApiKey!);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating an API key.");
            throw;
        }
    }

    public async Task<ApiKeyDto?> UpdateApiKeyAsync(UpdateApiKeyDto updateApiKeyDto, Guid uuid)
    {
        try
        {
            var existingApiKey = await _apiKeyRepository.GetByIdAsync(uuid);
            if (existingApiKey == null)
            {
                return null;
            }

            ApiKey updateApiKey = ApiKeyMapper.MapToEntity(updateApiKeyDto, existingApiKey);
            await _apiKeyRepository.UpdateAsync(updateApiKey);
            return ApiKeyMapper.MapToDto(updateApiKey);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating API key with UUID {UUID}.", uuid);
            throw;
        }
    }

    public async Task<bool> DeleteApiKeyAsync(Guid uuid)
    {
        try
        {
            var wasDeleted = await _apiKeyRepository.DeleteAsync(uuid);
            if (!wasDeleted)
            {
                _logger.LogWarning("Failed to delete API key with UUID {UUID}.", uuid);
            }
            return wasDeleted;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting API key with UUID {UUID}.", uuid);
            throw;
        }
    }

    public async Task<ApiKeyDto?> GetByTokenAsync(string apiKey)
    {
        try
        {
            ApiKey? apiKeyEntity = await _apiKeyRepository.GetByTokenAsync(apiKey);
            return apiKeyEntity is not null ? ApiKeyMapper.MapToDto(apiKeyEntity) : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving API key with token {apiKey}.", apiKey);
            throw;
        }
    }
}
