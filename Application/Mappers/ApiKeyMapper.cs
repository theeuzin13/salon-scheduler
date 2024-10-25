using Scheduler.Application.DTOs.ApiKey;
using Scheduler.Domain.Entities;

namespace Scheduler.Application.Mappers;

public class ApiKeyMapper
{
    public static ApiKeyDto MapToDto(ApiKey apiKeyEntity)
    {
        return new()
        {
            Uuid = apiKeyEntity.Uuid,
            Name = apiKeyEntity.Name,
            Description = apiKeyEntity.Description,
            Token = apiKeyEntity.Token,
        };
    }

    public static ApiKey MapToEntity(CreateApiKeyDto createApiKeyDto)
    {
        return new ApiKey()
        {
            Name = createApiKeyDto.Name,
            Description = createApiKeyDto.Description,
            Token = createApiKeyDto.Token,
        };
    }

    public static ApiKey MapToEntity(UpdateApiKeyDto updateApiKeyDto, ApiKey apiKeyEntity)
    {
        apiKeyEntity.Name = updateApiKeyDto.Name ?? apiKeyEntity.Name;
        apiKeyEntity.Description = updateApiKeyDto?.Description ?? apiKeyEntity.Description;
        apiKeyEntity.Token = updateApiKeyDto?.Token ?? apiKeyEntity.Token;

        return apiKeyEntity;
    }
}
