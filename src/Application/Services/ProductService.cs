using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        var products = await _unitOfWork.Repository<Product>().GetAllAsync();

        return products.Select(p => new ProductDto
        {
            Id = p.Id,
            ProductName = p.ProductName,
            Items = p.Items?.Select(i => new ItemDto
            {
                Id = i.Id,
                ProductId = i.ProductId,
                Quantity = i.Quantity
            }).ToList() ?? new List<ItemDto>()
        });
    }

    public async Task<ProductDto?> GetProductByIdAsync(int id)
    {
        var product = await _unitOfWork.Repository<Product>().GetByIdAsync(id);
        if (product == null) return null;

        return new ProductDto
        {
            Id = product.Id,
            ProductName = product.ProductName,
            Items = product.Items?.Select(i => new ItemDto
            {
                Id = i.Id,
                ProductId = i.ProductId,
                Quantity = i.Quantity
            }).ToList() ?? new List<ItemDto>()
        };
    }

    public async Task<ProductDto> CreateProductAsync(CreateProductDto productDto, string userId)
    {
        var product = new Product
        {
            ProductName = productDto.ProductName,
            CreatedBy = userId
        };

        await _unitOfWork.Repository<Product>().AddAsync(product);
        await _unitOfWork.SaveAsync();

        return new ProductDto
        {
            Id = product.Id,
            ProductName = product.ProductName
        };
    }

    public async Task UpdateProductAsync(int id, UpdateProductDto productDto, string userId)
    {
        var product = await _unitOfWork.Repository<Product>().GetByIdAsync(id);
        if (product == null) throw new Exception("Product not found");

        product.ProductName = productDto.ProductName;
        product.ModifiedBy = userId;
        product.ModifiedOn = DateTime.UtcNow;

        _unitOfWork.Repository<Product>().Update(product);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteProductAsync(int id)
    {
        var product = await _unitOfWork.Repository<Product>().GetByIdAsync(id);
        if (product == null) throw new Exception("Product not found");

        _unitOfWork.Repository<Product>().Delete(product);
        await _unitOfWork.SaveAsync();
    }
}
