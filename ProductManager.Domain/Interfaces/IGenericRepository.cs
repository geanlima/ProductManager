namespace ProductManager.Domain.Interfaces;

public interface IGenericRepository<T>
{
    Task<T> GetByIdRepositoryAsync(int id);
    Task<IEnumerable<T>> GetAllRepositoryAsync();
    Task AddRepositoryAsync(T entity);
    Task UpdateRepositoryAsync(T entity);
    Task DeleteRepositoryAsync(int id);
}
