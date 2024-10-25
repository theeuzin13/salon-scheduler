namespace Scheduler.Domain.Entities;

public class BaseEntity
{
    public virtual Guid Uuid { get; set; }
    public virtual DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public virtual DateTime UpdatedAt {  get; set; } = DateTime.UtcNow;
    public virtual DateTime? DeletedAt {  get; set; }

    public BaseEntity()
    {
        Uuid = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateTimeStamps()
    {
        UpdatedAt = DateTime.UtcNow;
    }

    public void SoftDelete()
    {
        DeletedAt = DateTime.UtcNow;
    }

    public void Restore()
    {
        DeletedAt = null;
        UpdateTimeStamps();
    }
}
