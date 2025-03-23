using WebApi.Models;
using WebApi.Utilities;

namespace WebApi.Services;
public interface ITenantService
{
    Task<Result<IEnumerable<TenantResponse>>> GetAllAsync();
    Task<Result<TenantResponse>> GetByIdAsync(string id);
    Task<Result<TenantResponse>> CreateAsync(TenantRequest request);
    Task<Result<TenantResponse>> UpdateAsync(string id, TenantRequest request);
    Task<Result<bool>> DeleteAsync(string id);
    Task<Result<TenantResponse>> GetByEmailAsync(string email);
}
