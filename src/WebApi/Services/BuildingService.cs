using WebApi.Models;
using WebApi.Repositories;
using WebApi.Utilities;

namespace WebApi.Services;
public class BuildingService(IRepositery<Building> buildingRepository) : IBuildingService
{
    public async Task<Result<IEnumerable<BuildingResponse>>> GetAllAsync()
    {
        var result = await buildingRepository.GetAll();
        if (!result.IsSuccess || result.Value == null)
        {
            return Result<IEnumerable<BuildingResponse>>.Failure(result.ErrorMessage ?? "Failed to retrieve buildings.");
        }

        var buildingResponses = result.Value.Select(MapToBuildingResponse).ToList();
        return Result<IEnumerable<BuildingResponse>>.Success(buildingResponses);
    }

    public async Task<Result<BuildingResponse>> GetByIdAsync(string id)
    {
        var result = await buildingRepository.GetById(id);
        if (!result.IsSuccess || result.Value == null)
        {
            return Result<BuildingResponse>.Failure(result.ErrorMessage ?? "Building not found.");
        }

        var buildingResponse = MapToBuildingResponse(result.Value);
        return Result<BuildingResponse>.Success(buildingResponse);
    }

    public async Task<Result<BuildingResponse>> CreateAsync(BuildingRequest request)
    {
        var newBuilding = new Building
        {
            Name = request.Name,
            Address = request.Address,
            NumberOfUnits = request.NumberOfUnits
        };

        var result = await buildingRepository.Create(newBuilding);
        if (!result.IsSuccess || result.Value == null)
        {
            return Result<BuildingResponse>.Failure(result.ErrorMessage ?? "Failed to create building.");
        }

        var buildingResponse = MapToBuildingResponse(result.Value);
        return Result<BuildingResponse>.Success(buildingResponse);
    }

    public async Task<Result<BuildingResponse>> UpdateAsync(string id, BuildingRequest request)
    {
        var existingBuildingResult = await buildingRepository.GetById(id);
        if (!existingBuildingResult.IsSuccess || existingBuildingResult.Value == null)
        {
            return Result<BuildingResponse>.Failure("Building not found.");
        }

        var existingBuilding = existingBuildingResult.Value;
        existingBuilding.Name = request.Name;
        existingBuilding.Address = request.Address;
        existingBuilding.NumberOfUnits = request.NumberOfUnits;

        var updateResult = await buildingRepository.Update(id, existingBuilding);
        if (!updateResult.IsSuccess || updateResult.Value == null)
        {
            return Result<BuildingResponse>.Failure(updateResult.ErrorMessage ?? "Failed to update building.");
        }

        var buildingResponse = MapToBuildingResponse(updateResult.Value);
        return Result<BuildingResponse>.Success(buildingResponse);
    }

    public async Task<Result<bool>> DeleteAsync(string id)
    {
        var result = await buildingRepository.Delete(id);
        if (!result.IsSuccess || result.Value == null)
        {
            return Result<bool>.Failure(result.ErrorMessage ?? "Failed to delete building.");
        }

        return Result<bool>.Success(true);
    }

    private BuildingResponse MapToBuildingResponse(Building building)
    {
        return new BuildingResponse
        {
            Id = building.Id,
            Name = building.Name,
            Address = building.Address,
            NumberOfUnits = building.NumberOfUnits
        };
    }

}
