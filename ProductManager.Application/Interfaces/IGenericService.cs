namespace ProductManager.Application.Interfaces;

public interface IGenericService<T>
{
    Task<T> GetByIdServiceAsync(int id);
    Task<IEnumerable<T>> GetAllServiceAsync();
    Task CreateServiceAsync(T entity);
    Task UpdateServiceAsync(T entity);
    Task DeleteServiceAsync(int id);
}
