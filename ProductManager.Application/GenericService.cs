using ProductManager.Application.Interfaces;
using ProductManager.Domain.Interfaces;

namespace ProductManager.Application;

public class GenericService<T> : IGenericService<T>
{
    private readonly IGenericRepository<T> _repository;

    public GenericService(IGenericRepository<T> repository)
    {
        _repository = repository;
    }

    public async Task<T> GetByIdServiceAsync(int id)
    {
        return await _repository.GetByIdRepositoryAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllServiceAsync()
    {
        return await _repository.GetAllRepositoryAsync();
    }

    public async Task CreateServiceAsync(T entity)
    {
        await _repository.AddRepositoryAsync(entity);
    }

    public async Task UpdateServiceAsync(T entity)
    {
        await _repository.UpdateRepositoryAsync(entity);
    }

    public async Task DeleteServiceAsync(int id)
    {
        await _repository.DeleteRepositoryAsync(id);
    }
}
