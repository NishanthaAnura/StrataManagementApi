using Microsoft.EntityFrameworkCore;
using WebApi.DataAccess;
using WebApi.Models;
using WebApi.Utilities;

namespace WebApi.Repositories;
public class OwnerRepository(StrataManagementDbContext context) : IOwnerRepository
{
    public async Task<Result<IEnumerable<Owner>>> GetAll()
    {
        try
        {
            var owners = await context.Owners
                .Include(o => o.AssignedBuilding)
                .ToListAsync();

            return Result<IEnumerable<Owner>>.Success(owners);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<Owner>>.Failure($"Failed to retrieve owners: {ex.Message}");
        }
    }

    public async Task<Result<Owner>> GetById(string id)
    {
        try
        {
            var owner = await context.Owners
                .Include(o => o.AssignedBuilding)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (owner == null)
            {
                return Result<Owner>.Failure("Owner not found.");
            }

            return Result<Owner>.Success(owner);
        }
        catch (Exception ex)
        {
            return Result<Owner>.Failure($"Failed to retrieve owner: {ex.Message}");
        }
    }

    public async Task<Result<Owner>> Create(Owner entity)
    {
        try
        {
            await context.Owners.AddAsync(entity);
            await context.SaveChangesAsync();

            var ownerWithBuilding = await context.Owners
                .Include(o => o.AssignedBuilding)
                .FirstOrDefaultAsync(o => o.Id == entity.Id);

            if (ownerWithBuilding == null)
            {
                return Result<Owner>.Failure("Owner's building not found.");
            }

            return Result<Owner>.Success(ownerWithBuilding);
        }
        catch (Exception ex)
        {
            return Result<Owner>.Failure($"Failed to create owner: {ex.Message}");
        }
    }

    public async Task<Result<Owner>> Update(string id, Owner entity)
    {
        try
        {
            var existingEntity = await context.Owners.FindAsync(id);
            if (existingEntity == null)
            {
                return Result<Owner>.Failure("Owner not found.");
            }

            context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await context.SaveChangesAsync();

            return Result<Owner>.Success(existingEntity);
        }
        catch (Exception ex)
        {
            return Result<Owner>.Failure($"Failed to update owner: {ex.Message}");
        }
    }

    public async Task<Result<Owner>> Delete(string id)
    {
        try
        {
            var entity = await context.Owners.FindAsync(id);
            if (entity == null)
            {
                return Result<Owner>.Failure("Owner not found.");
            }

            context.Owners.Remove(entity);
            await context.SaveChangesAsync();

            return Result<Owner>.Success(entity);
        }
        catch (Exception ex)
        {
            return Result<Owner>.Failure($"Failed to delete owner: {ex.Message}");
        }
    }

    public async Task<Result<Owner>> GetByEmail(string email)
    {
        try
        {
            var owner = await context.Owners
                .Include(o => o.AssignedBuilding)
                .FirstOrDefaultAsync(o => o.Contact == email);

            if (owner == null)
            {
                return Result<Owner>.Failure("Owner not found.");
            }

            return Result<Owner>.Success(owner);
        }
        catch (Exception ex)
        {
            return Result<Owner>.Failure($"Failed to retrieve owner: {ex.Message}");
        }
    }

}
