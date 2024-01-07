using ProductManager.Domain.Enums;

namespace ProductManager.Application.QueryParameters;

public class ProductFilter
{
    public string? Description { get; set; }
    public ProductStatus Status { get; set; } = 0;
    public string? OrderBy { get; set; }
}
