using WebApi.Models;
using WebApi.Utilities;

namespace WebApi.Repositories;
public interface IOwnerRepository : IRepositery<Owner>
{
    Task<Result<Owner>> GetByEmail(string email);
}
