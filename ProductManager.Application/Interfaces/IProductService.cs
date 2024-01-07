using ProductManager.Application.DTOs;
using ProductManager.Application.QueryParameters;

namespace ProductManager.Application.Interfaces;

public interface IProductService : IGenericService<ProductDTO>
{
    Task<List<ProductDTO>> GetProductsServiceAsync(ProductFilter filter, PaginationParameters pagination);
    Task DeleteLogicServiceAsync(int id);
}
