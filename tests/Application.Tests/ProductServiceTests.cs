using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Moq;
using Xunit;

namespace Application.Tests;

public class ProductServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly ProductService _productService;

    public ProductServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _productService = new ProductService(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task GetProductByIdAsync_ShouldReturnProduct_WhenProductExists()
    {
        // Arrange
        var productId = 1;
        var product = new Product { Id = productId, ProductName = "Test Product" };
        _unitOfWorkMock.Setup(u => u.Repository<Product>().GetByIdAsync(productId))
            .ReturnsAsync(product);

        // Act
        var result = await _productService.GetProductByIdAsync(productId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(productId, result.Id);
        Assert.Equal("Test Product", result.ProductName);
    }

    [Fact]
    public async Task CreateProductAsync_ShouldAddProductAndSave()
    {
        // Arrange
        var dto = new CreateProductDto { ProductName = "New Product" };
        var userId = "user1";

        // Act
        await _productService.CreateProductAsync(dto, userId);

        // Assert
        _unitOfWorkMock.Verify(u => u.Repository<Product>().AddAsync(It.IsAny<Product>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveAsync(default), Times.Once);
    }
}
