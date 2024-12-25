using Infrastructure.ApiResponses;

namespace Infrastructure.Interfaces;

public interface IGenericService<T>
{
    Task<Response<List<T>>> GetAll();
    Task<Response<T>> GetById(int id);
    Task<Response<bool>> Create(T data);
    Task<Response<bool>> Update(T data);
    Task<Response<bool>> Delete(int id);
}