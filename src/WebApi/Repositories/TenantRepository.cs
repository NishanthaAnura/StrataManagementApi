using Microsoft.EntityFrameworkCore;
using WebApi.DataAccess;
using WebApi.Models;
using WebApi.Utilities;

namespace WebApi.Repositories;
public class TenantRepository(StrataManagementDbContext context) : ITenantRepositery
{
    public async Task<Result<IEnumerable<Tenant>>> GetAll()
    {
        try
        {
            var tenants = await context.Tenants
                .Include(t => t.Building)
                .ToListAsync();

            return Result<IEnumerable<Tenant>>.Success(tenants);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<Tenant>>.Failure($"Failed to retrieve tenants: {ex.Message}");
        }
    }

    public async Task<Result<Tenant>> GetById(string id)
    {
        try
        {
            var tenant = await context.Tenants
                .Include(t => t.Building)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tenant == null)
            {
                return Result<Tenant>.Failure("Tenant not found.");
            }

            return Result<Tenant>.Success(tenant);
        }
        catch (Exception ex)
        {
            return Result<Tenant>.Failure($"Failed to retrieve tenant: {ex.Message}");
        }
    }

    public async Task<Result<Tenant>> Create(Tenant entity)
    {
        try
        {
            await context.Tenants.AddAsync(entity);
            await context.SaveChangesAsync();

            var tenantWithBuilding = await context.Tenants
                .Include(o => o.Building)
                .FirstOrDefaultAsync(o => o.Id == entity.Id);

            if (tenantWithBuilding == null)
            {
                return Result<Tenant>.Failure("Tenant's building not found.");
            }

            return Result<Tenant>.Success(tenantWithBuilding);
        }
        catch (Exception ex)
        {
            return Result<Tenant>.Failure($"Failed to create tenant: {ex.Message}");
        }
    }

    public async Task<Result<Tenant>> Update(string id, Tenant entity)
    {
        try
        {
            var existingEntity = await context.Tenants.FindAsync(id);
            if (existingEntity == null)
            {
                return Result<Tenant>.Failure("Tenant not found.");
            }

            context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await context.SaveChangesAsync();

            return Result<Tenant>.Success(existingEntity);
        }
        catch (Exception ex)
        {
            return Result<Tenant>.Failure($"Failed to update tenant: {ex.Message}");
        }
    }

    public async Task<Result<Tenant>> Delete(string id)
    {
        try
        {
            var entity = await context.Tenants.FindAsync(id);
            if (entity == null)
            {
                return Result<Tenant>.Failure("Tenant not found.");
            }

            context.Tenants.Remove(entity);
            await context.SaveChangesAsync();

            return Result<Tenant>.Success(entity);
        }
        catch (Exception ex)
        {
            return Result<Tenant>.Failure($"Failed to delete tenant: {ex.Message}");
        }
    }

    public async Task<Result<Tenant>> GetByEmail(string email)
    {
        try
        {
            var owner = await context.Tenants
                .Include(o => o.Building)
                .FirstOrDefaultAsync(o => o.Contact == email);

            if (owner == null)
            {
                return Result<Tenant>.Failure("Owner not found.");
            }

            return Result<Tenant>.Success(owner);
        }
        catch (Exception ex)
        {
            return Result<Tenant>.Failure($"Failed to retrieve owner: {ex.Message}");
        }
    }
}
