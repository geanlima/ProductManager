using ProductManager.Domain.Enums;

namespace ProductManager.Application.DTOs;

public class ProductDTO
{
    public int Id { get; set; }
    public string Description { get; set; }
    public ProductStatus Status { get; set; }
    public DateTime ManufacturingDate { get; set; }
    public DateTime ExpiryDate { get; set; }
}
