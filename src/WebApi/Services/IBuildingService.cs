using WebApi.Models;
using WebApi.Utilities;

namespace WebApi.Services;
public interface IBuildingService
{
    Task<Result<IEnumerable<BuildingResponse>>> GetAllAsync();
    Task<Result<BuildingResponse>> GetByIdAsync(string id);
    Task<Result<BuildingResponse>> CreateAsync(BuildingRequest request);
    Task<Result<BuildingResponse>> UpdateAsync(string id, BuildingRequest request);
    Task<Result<bool>> DeleteAsync(string id);
}
