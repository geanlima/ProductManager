using AutoMapper;
using ProductManager.Application.DTOs;
using ProductManager.Application.Interfaces;
using ProductManager.Application.QueryParameters;
using ProductManager.Application.Validation;
using ProductManager.Domain.Entities;
using ProductManager.Domain.Enums;
using ProductManager.Domain.Interfaces;

namespace ProductManager.Application;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task CreateServiceAsync(ProductDTO entity)
    {
        var product = _mapper.Map<Products>(entity);

        ValidateProduct(product);

        await _repository.AddRepositoryAsync(product);
    }

    public async Task DeleteServiceAsync(int id)
    {
        await _repository.DeleteRepositoryAsync(id);
    }

    public async Task<IEnumerable<ProductDTO>> GetAllServiceAsync()
    {
        var products = await _repository.GetAllRepositoryAsync();
        var result = _mapper.Map<IEnumerable<ProductDTO>>(products);
        return result;
    }

    public async Task<ProductDTO> GetByIdServiceAsync(int id)
    {
        var product = await _repository.GetByIdRepositoryAsync(id);
        return _mapper.Map<ProductDTO>(product);
    }

    public async Task<List<ProductDTO>> GetProductsServiceAsync(ProductFilter filter, PaginationParameters pagination)
    {
        var products = await _repository.GetAllRepositoryAsync();


        if (!string.IsNullOrEmpty(filter.Description))
        {
            products = products.Where(p => p.Description.Contains(filter.Description)).ToList();
        }


        if (!string.IsNullOrEmpty(filter.OrderBy))
        {
            switch (filter.OrderBy)
            {
                case "description":
                    products = products.OrderBy(p => p.Description).ToList();
                    break;
                case "status":
                    products = products.OrderBy(p => p.Status).ToList();
                    break;
                    
            }
        }


        var totalCount = products.Count();
        var totalPages = (int)Math.Ceiling((double)totalCount / pagination.PageSize);

        var results = products
            .Skip((pagination.PageNumber - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToList();

        return results.Select(product => _mapper.Map<ProductDTO>(product)).ToList();
    }

    public async Task UpdateServiceAsync(ProductDTO entity)
    {
        var product = _mapper.Map<Products>(entity);

        ValidateProduct(product);

        await _repository.UpdateRepositoryAsync(product);
    }

    public async Task DeleteLogicServiceAsync(int id)
    {
        var product = await _repository.GetByIdRepositoryAsync(id);

        if (product == null)
        {
            throw new Exception("Produto não encontrado.");
        }

        product.Status = ProductStatus.Inactive; 

        await _repository.UpdateRepositoryAsync(product);
    }

    public void ValidateProduct(Products product)
    {
        if (!ProductValidation.IsManufacturingDateValid(product.ManufacturingDate, product.ExpiryDate))
        {
            throw new Exception("A data de fabricação não pode ser maior ou igual à data de validade.");
        }

        if (!ProductValidation.IsDescriptionValid(product.Description))
        {
            throw new Exception("A descrição não pode estar em branco.");
        }

        if (!ProductValidation.IsExpiryDateValid(product.ExpiryDate))
        {
            throw new Exception("A data de validade deve ser maior que a data atual.");
        }
    }
}
