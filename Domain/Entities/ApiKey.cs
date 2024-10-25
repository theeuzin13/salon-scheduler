namespace Scheduler.Domain.Entities;

public class ApiKey : BaseEntity
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required string Token { get; set; }
}
