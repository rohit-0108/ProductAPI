namespace Application.DTOs;

public class ProductDto
{
    public int Id { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public List<ItemDto> Items { get; set; } = new();
}

public class ItemDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}

public class CreateProductDto
{
    public string ProductName { get; set; } = string.Empty;
}

public class UpdateProductDto
{
    public string ProductName { get; set; } = string.Empty;
}
