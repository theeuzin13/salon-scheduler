using Microsoft.EntityFrameworkCore;
using Scheduler.Domain.Entities;
using Scheduler.Infrastructure.Mappings;


namespace Scheduler.Infrastructure;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<ApiKey> ApiKeys { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ApiKeyMapping());

        base.OnModelCreating(modelBuilder);
    }
}
