using Microsoft.AspNetCore.Mvc;
using Scheduler.Application.DTOs.ApiKey;
using Scheduler.Application.Services.Interfaces;

namespace Scheduler.Presentation;

[ApiController]
[Route("api/v1/apikey")]
public class ApiKeyController : ControllerBase
{
    private readonly IApiKeyService _apiKeyService;

    public ApiKeyController(IApiKeyService apiKeyService)
    {
        _apiKeyService = apiKeyService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            IEnumerable<ApiKeyDto?> apiKeyDtos = await _apiKeyService.GetAllApiKeyAsync();
            return Ok(apiKeyDtos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }

    }

    [HttpGet("{uuid}")]
    public async Task<IActionResult> GetByIdAsync(Guid uuid)
    {
        try
        {
            var apiKey = await _apiKeyService.GetApiKeyOrNullAsync(uuid);
            if (apiKey == null)
            {
                return NotFound();
            }
            return Ok(apiKey);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateApiKeyAsync(CreateApiKeyDto createApiKeyDto)
    {
        try
        {
            var apiKey = await _apiKeyService.CreateApiKeyAsync(createApiKeyDto);
            return StatusCode(201, apiKey);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut("{uuid}")]
    public async Task<IActionResult> UpdateApiKeyAsync(Guid uuid, UpdateApiKeyDto updateApiKeyDto)
    {
        try
        {
            var updatedApiKey = await _apiKeyService.UpdateApiKeyAsync(updateApiKeyDto, uuid);
            if (updatedApiKey == null)
            {
                return NotFound();
            }
            return Ok(updatedApiKey);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete("{uuid}")]
    public async Task<IActionResult> DeleteApiKeyAsync(Guid uuid)
    {
        try
        {
            var deletedApiKey = await _apiKeyService.DeleteApiKeyAsync(uuid);
            if (deletedApiKey == false)
            {
                return NotFound();
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }


}