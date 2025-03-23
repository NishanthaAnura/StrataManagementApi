using Microsoft.AspNetCore.Identity;
using WebApi.Models;
using WebApi.Repositories;
using WebApi.Utilities;

namespace WebApi.Services;
public class MaintenanceRequestService(
    IMaintenanceRequestRepository repository,
    UserManager<ApplicationUser> userManager) : IMaintenanceRequestService
{
    public async Task<Result<IEnumerable<MaintenanceRequestResponse>>> GetAllAsync()
    {
        var result = await repository.GetAllAsync();
        if (!result.IsSuccess || result.Value == null)
        {
            return Result<IEnumerable<MaintenanceRequestResponse>>.Failure(result.ErrorMessage);
        }

        var responses = result.Value.Select(MapToMaintenanceRequestResponse);
        return Result<IEnumerable<MaintenanceRequestResponse>>.Success(responses);
    }

    public async Task<Result<MaintenanceRequestResponse>> GetByIdAsync(string id)
    {
        var result = await repository.GetByIdAsync(id);
        if (!result.IsSuccess || result.Value == null)
        {
            return Result<MaintenanceRequestResponse>.Failure(result.ErrorMessage);
        }

        var response = MapToMaintenanceRequestResponse(result.Value);
        return Result<MaintenanceRequestResponse>.Success(response);
    }

    public async Task<Result<IEnumerable<MaintenanceRequestResponse>>> GetMaintenanceRequestsForUserAsync(RoleBaseMaintenenceRequest request)
    {
        if (UserRoles.Owner == request.Role && request.BuildingId != null)
        {
            var result = await repository.GetMaintenanceRequestsByBuildingIdAsync(request.BuildingId);
            if (!result.IsSuccess || result.Value == null)
            {
                return Result<IEnumerable<MaintenanceRequestResponse>>.Failure(result.ErrorMessage ?? "Not fount requests");
            }

            var responses = result.Value.Select(MapToMaintenanceRequestResponse);
            return Result<IEnumerable<MaintenanceRequestResponse>>.Success(responses);
        }
        else if (UserRoles.Tenant == request.Role && request.UnitNumber != null)
        {
            var result = await repository.GetMaintenanceRequestsUnitNumberAsync(request.UnitNumber);
            if (!result.IsSuccess || result.Value == null)
            {
                return Result<IEnumerable<MaintenanceRequestResponse>>.Failure(result.ErrorMessage ?? "Not fount requests");
            }

            var responses = result.Value.Select(MapToMaintenanceRequestResponse);
            return Result<IEnumerable<MaintenanceRequestResponse>>.Success(responses);
        }

        return Result<IEnumerable<MaintenanceRequestResponse>>.Failure("User role not recognized.");
    }

    public async Task<Result<MaintenanceRequestResponse>> CreateAsync(MaintenanceUserRequest request)
    {
        var newRequest = new MaintenanceRequest
        {
            Title = request.Title,
            Description = request.Description,
            Status = request.Status,
            BuildingId = request.BuildingId,
            UnitNumber = request.UnitNumber,
            LastChangedTime = DateTime.UtcNow,
        };
        var result = await repository.CreateAsync(newRequest);
        if (!result.IsSuccess || result.Value == null)
        {
            return Result<MaintenanceRequestResponse>.Failure(result.ErrorMessage ?? "Failed to create request.");
        }

        var response = MapToMaintenanceRequestResponse(result.Value);
        return Result<MaintenanceRequestResponse>.Success(response);
    }

    public async Task<Result<MaintenanceRequestResponse>> UpdateAsync(string id, MaintenanceUpdatedRequest request)
    {
        var existingRequestResult = await repository.GetByIdAsync(id);
        if (!existingRequestResult.IsSuccess || existingRequestResult.Value == null)
        {
            return Result<MaintenanceRequestResponse>.Failure("Maintenance request not found.");
        }

        var existingRequest = existingRequestResult.Value;
        existingRequest.Status = request.Status;
        existingRequest.LastChangedTime = DateTime.UtcNow;

        var result = await repository.UpdateAsync(id, existingRequest);
        if (!result.IsSuccess || result.Value == null)
        {
            return Result<MaintenanceRequestResponse>.Failure(result.ErrorMessage ?? "Failed to update request.");
        }

        var response = MapToMaintenanceRequestResponse(result.Value);
        return Result<MaintenanceRequestResponse>.Success(response);
    }

    public async Task<Result<bool>> DeleteAsync(string id)
    {
        var result = await repository.DeleteAsync(id);
        if (!result.IsSuccess)
        {
            return Result<bool>.Failure(result.ErrorMessage ?? "Failed to delete request.");
        }

        return Result<bool>.Success(true);
    }


    private MaintenanceRequestResponse MapToMaintenanceRequestResponse(MaintenanceRequest request)
    {
        return new MaintenanceRequestResponse
        {
            Id = request.Id,
            Title = request.Title,
            Description = request.Description,
            Status = request.Status,
            LastChangedTime = request.LastChangedTime,
            BuildingName = request.Building?.Name ?? "Unknown",
            UnitNumber = request.UnitNumber
        };
    }


}
