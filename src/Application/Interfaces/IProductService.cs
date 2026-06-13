using Application.DTOs;

namespace Application.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllProductsAsync();
    Task<ProductDto?> GetProductByIdAsync(int id);
    Task<ProductDto> CreateProductAsync(CreateProductDto productDto, string userId);
    Task UpdateProductAsync(int id, UpdateProductDto productDto, string userId);
    Task DeleteProductAsync(int id);
}
