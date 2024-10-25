namespace Scheduler.Application.DTOs.ApiKey;

public record ApiKeyDto
{
    public required Guid Uuid { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required string Token { get; set; }
}
