using ProductManager.Domain.Enums;

namespace ProductManager.Domain.Entities;

public class Products
{
    public int Id { get; set; }
    public string Description { get; set; }
    public ProductStatus Status { get; set; }
    public DateTime ManufacturingDate { get; set; }
    public DateTime ExpiryDate { get; set; }
}
