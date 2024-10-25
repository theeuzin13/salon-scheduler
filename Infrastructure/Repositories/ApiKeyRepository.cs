using Microsoft.EntityFrameworkCore;
using Scheduler.Domain.Entities;
using Scheduler.Domain.Interfaces;

namespace Scheduler.Infrastructure.Repositories;

public class ApiKeyRepository(ApplicationDbContext context) : IApiKeyRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<ApiKey?> GetByIdAsync(Guid uuid)
    {
        return await _context.ApiKeys
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Uuid == uuid && x.DeletedAt == null);
    }

    public async Task<IEnumerable<ApiKey?>> GetAllAsync()
    {
        return await _context.ApiKeys.AsNoTracking().Where(x => x.DeletedAt == null).ToListAsync();
    }

    public async Task<ApiKey?> CreateAsync(ApiKey apiKey)
    {
        await _context.ApiKeys.AddAsync(apiKey);
        await _context.SaveChangesAsync();
        return apiKey;
    }

    public async Task<ApiKey?> UpdateAsync(ApiKey apiKey)
    {
        _context.ApiKeys.Update(apiKey);
        await _context.SaveChangesAsync();
        return apiKey;
    }

    public async Task<bool> DeleteAsync(Guid uuid)
    {
        var entity = await _context.ApiKeys
            .FirstOrDefaultAsync(x => x.Uuid == uuid && x.DeletedAt == null);

        if (entity == null)
        {
            return false;
        }

        entity.SoftDelete();

        await UpdateAsync(entity);

        return true;
    }

    public async Task<ApiKey?> GetByTokenAsync(string apiKey)
    {
        return await _context.ApiKeys.FirstOrDefaultAsync(a => a.Token == apiKey && a.DeletedAt == null);
    }
}
