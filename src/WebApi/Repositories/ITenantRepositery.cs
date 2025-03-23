using WebApi.Models;
using WebApi.Utilities;

namespace WebApi.Repositories;
public interface ITenantRepositery : IRepositery<Tenant>
{
    Task<Result<Tenant>> GetByEmail(string email);
}
