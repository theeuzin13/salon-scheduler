using Scheduler.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Scheduler.Infrastructure.Mappings;

public class ApiKeyMapping : IEntityTypeConfiguration<ApiKey>
{
    public void Configure(EntityTypeBuilder<ApiKey> builder)
    {
        builder.ToTable("api_key");

        builder.HasKey(x => x.Uuid);

        builder.Property(x => x.Name)
            .HasColumnName("name")
            .IsRequired();
        builder.Property(x => x.Description)
            .HasColumnName("description");
        builder.Property(x => x.Token)
            .HasColumnName("token")
            .IsRequired();
        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired();

        builder.Property(x => x.DeletedAt)
            .HasColumnName("deleted_at");

        builder.HasData(
            new ApiKey
            {
                Name = "master",
                Description = "first apikey",
                Token = "W8aKo6fhzZoWBECW86Gt7osp5i2UAp5Rs3JAbpFMRLSaQPgu9Hc4hHVpEepkm5MW",
            });
    }
}
