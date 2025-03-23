using WebApi.Models;
using WebApi.Utilities;

namespace WebApi.Repositories;
public interface IMaintenanceRequestRepository
{
    Task<Result<IEnumerable<MaintenanceRequest>>> GetAllAsync();
    Task<Result<MaintenanceRequest>> GetByIdAsync(string id);
    Task<Result<IEnumerable<MaintenanceRequest>>> GetMaintenanceRequestsByBuildingIdAsync(string buildingId);
    Task<Result<IEnumerable<MaintenanceRequest>>> GetMaintenanceRequestsUnitNumberAsync(string unitNumber);
    Task<Result<MaintenanceRequest>> CreateAsync(MaintenanceRequest entity);
    Task<Result<MaintenanceRequest>> UpdateAsync(string id, MaintenanceRequest entity);
    Task<Result<MaintenanceRequest>> DeleteAsync(string id);
    //Task<Result<Owner>> GetOwnerByUserIdAsync(string userId);

}
