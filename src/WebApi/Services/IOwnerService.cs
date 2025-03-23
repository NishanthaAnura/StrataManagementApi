using WebApi.Models;
using WebApi.Utilities;

namespace WebApi.Services;
public interface IOwnerService
{
    Task<Result<IEnumerable<OwnerResponse>>> GetAllAsync();
    Task<Result<OwnerResponse>> GetByIdAsync(string id);
    Task<Result<OwnerResponse>> CreateAsync(OwnerRequest request);
    Task<Result<OwnerResponse>> UpdateAsync(string id, OwnerRequest request);
    Task<Result<bool>> DeleteAsync(string id);
    Task<Result<OwnerResponse>> GetByEmailAsync(string email);
}
