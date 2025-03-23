using Microsoft.EntityFrameworkCore;
using WebApi.DataAccess;
using WebApi.Models;
using WebApi.Utilities;

namespace WebApi.Repositories;
public class MaintenanceRequestRepository(StrataManagementDbContext context) : IMaintenanceRequestRepository
{
    public async Task<Result<IEnumerable<MaintenanceRequest>>> GetAllAsync()
    {
        try
        {
            var requests = await context.MaintenanceRequests
                //.Include(mr => mr.CreatedBy)
                .Include(mr => mr.Building)
                .ToListAsync();

            return Result<IEnumerable<MaintenanceRequest>>.Success(requests);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<MaintenanceRequest>>.Failure($"Failed to retrieve maintenance requests: {ex.Message}");
        }
    }

    public async Task<Result<MaintenanceRequest>> GetByIdAsync(string id)
    {
        try
        {
            var request = await context.MaintenanceRequests
                //.Include(mr => mr.CreatedBy)
                .Include(mr => mr.Building)
                .FirstOrDefaultAsync(mr => mr.Id == id);

            if (request == null)
            {
                return Result<MaintenanceRequest>.Failure("Maintenance request not found.");
            }

            return Result<MaintenanceRequest>.Success(request);
        }
        catch (Exception ex)
        {
            return Result<MaintenanceRequest>.Failure($"Failed to retrieve maintenance request: {ex.Message}");
        }
    }

    public async Task<Result<IEnumerable<MaintenanceRequest>>> GetMaintenanceRequestsByBuildingIdAsync(string buildingId)
    {
        try
        {
            var requests = await context.MaintenanceRequests
                //.Include(mr => mr.CreatedBy)
                .Include(mr => mr.Building)
                .Where(mr => mr.BuildingId == buildingId)
                .ToListAsync();

            return Result<IEnumerable<MaintenanceRequest>>.Success(requests);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<MaintenanceRequest>>.Failure($"Failed to retrieve maintenance requests by building Id: {ex.Message}");
        }
    }

    public async Task<Result<IEnumerable<MaintenanceRequest>>> GetMaintenanceRequestsUnitNumberAsync(string unitNumber)
    {
        try
        {
            var requests = await context.MaintenanceRequests
                //.Include(mr => mr.CreatedBy)
                .Include(mr => mr.Building)
                .Where(mr => mr.UnitNumber != null && mr.UnitNumber == unitNumber)
                .ToListAsync();

            return Result<IEnumerable<MaintenanceRequest>>.Success(requests);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<MaintenanceRequest>>.Failure($"Failed to retrieve maintenance requests by unit number: {ex.Message}");
        }
    }

    public async Task<Result<MaintenanceRequest>> CreateAsync(MaintenanceRequest entity)
    {
        try
        {
            await context.MaintenanceRequests.AddAsync(entity);
            await context.SaveChangesAsync();

            var request = await context.MaintenanceRequests
                //.Include(mr => mr.CreatedBy)
                .Include(mr => mr.Building)
                .FirstOrDefaultAsync(i => i.Id == entity.Id);

            return Result<MaintenanceRequest>.Success(entity);
        }
        catch (Exception ex)
        {
            return Result<MaintenanceRequest>.Failure($"Failed to create maintenance request: {ex.Message}");
        }
    }

    public async Task<Result<MaintenanceRequest>> UpdateAsync(string id, MaintenanceRequest entity)
    {
        try
        {
            var existingEntity = await context.MaintenanceRequests.FindAsync(id);
            if (existingEntity == null)
            {
                return Result<MaintenanceRequest>.Failure("Maintenance request not found.");
            }

            context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await context.SaveChangesAsync();

            return Result<MaintenanceRequest>.Success(existingEntity);
        }
        catch (Exception ex)
        {
            return Result<MaintenanceRequest>.Failure($"Failed to update maintenance request: {ex.Message}");
        }
    }

    public async Task<Result<MaintenanceRequest>> DeleteAsync(string id)
    {
        try
        {
            var entity = await context.MaintenanceRequests.FindAsync(id);
            if (entity == null)
            {
                return Result<MaintenanceRequest>.Failure("Maintenance request not found.");
            }

            context.MaintenanceRequests.Remove(entity);
            await context.SaveChangesAsync();

            return Result<MaintenanceRequest>.Success(entity);
        }
        catch (Exception ex)
        {
            return Result<MaintenanceRequest>.Failure($"Failed to delete maintenance request: {ex.Message}");
        }
    }

    //public async Task<Result<Owner>> GetOwnerByUserIdAsync(string userId)
    //{
    //    try
    //    {
    //        var owner = await context.Owners
    //            .Include(o => o.User)
    //            .FirstOrDefaultAsync(o => o.UserId == userId);

    //        if (owner == null)
    //        {
    //            return Result<Owner>.Failure("Owner not found.");
    //        }

    //        return Result<Owner>.Success(owner);
    //    }
    //    catch (Exception ex)
    //    {
    //        return Result<Owner>.Failure($"Failed to retrieve owner by user ID: {ex.Message}");
    //    }
    //}

}
