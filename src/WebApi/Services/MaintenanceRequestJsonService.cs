using WebApi.Models;
using WebApi.Repositories;
using WebApi.Utilities;

namespace WebApi.Services;
public class MaintenanceRequestJsonService(IRepositery<MaintenanceRequest> repository)
{
    public async Task<Result<IEnumerable<MaintenanceRequest>>> GetAllRequests() => await repository.GetAll();
    public async Task<Result<MaintenanceRequest>> GetRequestById(string id) => await repository.GetById(id);
    public async Task<Result<MaintenanceRequest>> CreateRequest(MaintenanceRequest request) => await repository.Create(request);
    public async Task<Result<MaintenanceRequest>> UpdateRequest(string id, MaintenanceRequest request) => await repository.Update(id, request);
    public async Task<Result<MaintenanceRequest>> DeleteRequest(string id) => await repository.Delete(id);

}