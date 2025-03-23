using WebApi.Models;
using WebApi.Repositories;
using WebApi.Utilities;

namespace WebApi.Services;
public class OwnerService(IOwnerRepository ownerRepository) : IOwnerService
{
    public async Task<Result<IEnumerable<OwnerResponse>>> GetAllAsync()
    {
        var result = await ownerRepository.GetAll();
        if (!result.IsSuccess || result.Value == null)
        {
            return Result<IEnumerable<OwnerResponse>>.Failure(result.ErrorMessage ?? "Failed to retrieve owners.");
        }

        var ownerResponses = result.Value.Select(MapToOwnerResponse).ToList();
        return Result<IEnumerable<OwnerResponse>>.Success(ownerResponses);
    }


    public async Task<Result<OwnerResponse>> GetByIdAsync(string id)
    {
        var result = await ownerRepository.GetById(id);
        if (!result.IsSuccess || result.Value == null)
        {
            return Result<OwnerResponse>.Failure(result.ErrorMessage ?? "Owner not found.");
        }

        var ownerResponse = MapToOwnerResponse(result.Value);
        return Result<OwnerResponse>.Success(ownerResponse);
    }

    public async Task<Result<OwnerResponse>> CreateAsync(OwnerRequest request)
    {
        var newOwner = new Owner
        {
            Name = request.Name,
            Contact = request.Contact,
            AssignedBuildingId = request.AssignedBuildingId,
            //UserId = request.UserId
        };

        var result = await ownerRepository.Create(newOwner);
        if (!result.IsSuccess || result.Value == null)
        {
            return Result<OwnerResponse>.Failure(result.ErrorMessage ?? "Failed to create owner.");
        }

        var ownerResponse = MapToOwnerResponse(result.Value);
        return Result<OwnerResponse>.Success(ownerResponse);
    }

    public async Task<Result<OwnerResponse>> UpdateAsync(string id, OwnerRequest request)
    {
        var existingOwnerResult = await ownerRepository.GetById(id);
        if (!existingOwnerResult.IsSuccess || existingOwnerResult.Value == null)
        {
            return Result<OwnerResponse>.Failure("Owner not found.");
        }

        var existingOwner = existingOwnerResult.Value;
        existingOwner.Name = request.Name;
        existingOwner.Contact = request.Contact;
        existingOwner.AssignedBuildingId = request.AssignedBuildingId;

        var updateResult = await ownerRepository.Update(id, existingOwner);
        if (!updateResult.IsSuccess || updateResult.Value == null)
        {
            return Result<OwnerResponse>.Failure(updateResult.ErrorMessage ?? "Failed to update owner.");
        }

        var ownerResponse = MapToOwnerResponse(updateResult.Value);
        return Result<OwnerResponse>.Success(ownerResponse);
    }

    public async Task<Result<bool>> DeleteAsync(string id)
    {
        var result = await ownerRepository.Delete(id);
        if (!result.IsSuccess || result.Value == null)
        {
            return Result<bool>.Failure(result.ErrorMessage ?? "Failed to delete owner.");
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<OwnerResponse>> GetByEmailAsync(string email)
    {
        var result = await ownerRepository.GetByEmail(email);
        if (!result.IsSuccess || result.Value == null)
        {
            return Result<OwnerResponse>.Failure(result.ErrorMessage ?? "Owner not found.");
        }

        var ownerResponse = MapToOwnerResponse(result.Value);
        return Result<OwnerResponse>.Success(ownerResponse);
    }

    private OwnerResponse MapToOwnerResponse(Owner owner)
    {
        return new OwnerResponse
        {
            Id = owner.Id,
            Name = owner.Name,
            Contact = owner.Contact,
            AssignedBuildingId = owner.AssignedBuildingId,
            AssignedBuilding = new BuildingResponse
            {
                Id = owner.AssignedBuilding.Id,
                Name = owner.AssignedBuilding.Name,
                Address = owner.AssignedBuilding.Address,
                NumberOfUnits = owner.AssignedBuilding.NumberOfUnits
            },
            MaintenanceRequests = owner.MaintenanceRequests
                .Where(mr => mr.BuildingId == owner.AssignedBuildingId)
                .Select(mr => new MaintenanceRequestResponse
                {
                    Id = mr.Id,
                    Title = mr.Title,
                    Description = mr.Description,
                    Status = mr.Status,
                    LastChangedTime = mr.LastChangedTime,
                    //UserName = mr.CreatedBy?.FullName ?? "Unknown",
                    BuildingName = mr.Building?.Name ?? "Unknown"
                })
                .ToList()
        };

    }

}
