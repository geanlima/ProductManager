using AutoMapper;
using Moq;
using ProductManager.Application;
using ProductManager.Application.DTOs;
using ProductManager.Application.Interfaces;
using ProductManager.Domain.Entities;
using ProductManager.Domain.Enums;
using ProductManager.Domain.Interfaces;

namespace ProductManager.Tests;

public class ProductServiceTests
{
    private readonly IProductService _productService;
    private readonly Mock<IProductRepository> _mockProductRepository;
    private readonly Mock<IMapper> _mockMapper;

    public ProductServiceTests()
    {
        _mockProductRepository = new Mock<IProductRepository>();
        _mockMapper = new Mock<IMapper>();

        _productService = new ProductService(_mockProductRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task CreateProductAsync_ValidProduct_ShouldSucceed()
    {
        // Arrange
        var productDto = new ProductDTO
        {
            Description = "Test Product",
            ManufacturingDate = DateTime.Now.AddMonths(-1),
            ExpiryDate = DateTime.Now.AddMonths(1)
        };
        var product = new Products
        {
            Id = 1,
            Description = "Test Product",
            ManufacturingDate = DateTime.Now.AddMonths(-1),
            ExpiryDate = DateTime.Now.AddMonths(1)
        };

        _mockMapper.Setup(m => m.Map<Products>(It.IsAny<ProductDTO>())).Returns(product);

        // Act
        await _productService.CreateServiceAsync(productDto);

        // Assert
        _mockProductRepository.Verify(r => r.AddRepositoryAsync(product), Times.Once);
    }

    [Fact]
    public async Task CreateProductAsync_InvalidManufacturingDate_ShouldThrowException()
    {
        // Arrange
        var productDto = new ProductDTO
        {
            Description = "Test Product",
            Status = ProductStatus.Active,
            ManufacturingDate = DateTime.Now.AddMonths(1),
            ExpiryDate = DateTime.Now.AddMonths(-1)
        };

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _productService.CreateServiceAsync(productDto));
    }


}
