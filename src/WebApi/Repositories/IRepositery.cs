using WebApi.Utilities;

namespace WebApi.Repositories;
public interface IRepositery<T> where T : class
{
    Task<Result<IEnumerable<T>>> GetAll();
    Task<Result<T>> GetById(string id);
    Task<Result<T>> Create(T entity);
    Task<Result<T>> Update(string id, T entity);
    Task<Result<T>> Delete(string id);
}
