using WebApi.Models;
using WebApi.Utilities;

namespace WebApi.Services;
public interface IMaintenanceRequestService
{
    Task<Result<IEnumerable<MaintenanceRequestResponse>>> GetAllAsync();
    Task<Result<MaintenanceRequestResponse>> GetByIdAsync(string id);
    Task<Result<IEnumerable<MaintenanceRequestResponse>>> GetMaintenanceRequestsForUserAsync(RoleBaseMaintenenceRequest request);
    Task<Result<MaintenanceRequestResponse>> CreateAsync(MaintenanceUserRequest request);
    Task<Result<MaintenanceRequestResponse>> UpdateAsync(string id, MaintenanceUpdatedRequest status);
    Task<Result<bool>> DeleteAsync(string id);
}
