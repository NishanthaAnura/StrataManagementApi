using Microsoft.EntityFrameworkCore;
using WebApi.DataAccess;
using WebApi.Models;
using WebApi.Utilities;

namespace WebApi.Repositories;
public class BuildingRepository(StrataManagementDbContext context) : IRepositery<Building>
{
    public async Task<Result<IEnumerable<Building>>> GetAll()
    {
        try
        {
            var buildings = await context.Buildings
                .Include(b => b.Owners)
                .Include(b => b.Tenants)
                .Include(b => b.MaintenanceRequests)
                .ToListAsync();

            return Result<IEnumerable<Building>>.Success(buildings);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<Building>>.Failure($"Failed to retrieve buildings: {ex.Message}");
        }
    }

    public async Task<Result<Building>> GetById(string id)
    {
        try
        {
            var building = await context.Buildings
                .Include(b => b.Owners)
                .Include(b => b.Tenants)
                .Include(b => b.MaintenanceRequests)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (building == null)
            {
                return Result<Building>.Failure("Building not found.");
            }

            return Result<Building>.Success(building);
        }
        catch (Exception ex)
        {
            return Result<Building>.Failure($"Failed to retrieve building: {ex.Message}");
        }
    }

    public async Task<Result<Building>> Create(Building entity)
    {
        try
        {
            await context.Buildings.AddAsync(entity);
            await context.SaveChangesAsync();

            return Result<Building>.Success(entity);
        }
        catch (Exception ex)
        {
            return Result<Building>.Failure($"Failed to create building: {ex.Message}");
        }
    }

    public async Task<Result<Building>> Update(string id, Building entity)
    {
        try
        {
            var existingEntity = await context.Buildings.FindAsync(id);
            if (existingEntity == null)
            {
                return Result<Building>.Failure("Building not found.");
            }

            context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await context.SaveChangesAsync();

            return Result<Building>.Success(existingEntity);
        }
        catch (Exception ex)
        {
            return Result<Building>.Failure($"Failed to update building: {ex.Message}");
        }
    }

    public async Task<Result<Building>> Delete(string id)
    {
        try
        {
            var entity = await context.Buildings.FindAsync(id);
            if (entity == null)
            {
                return Result<Building>.Failure("Building not found.");
            }

            context.Buildings.Remove(entity);
            await context.SaveChangesAsync();

            return Result<Building>.Success(entity);
        }
        catch (Exception ex)
        {
            return Result<Building>.Failure($"Failed to delete building: {ex.Message}");
        }
    }
}
