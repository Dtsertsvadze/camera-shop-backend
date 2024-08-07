using camera_shop.Core.Entities;

namespace camera_shop.Core.DTO;

public class ProductResponse
{
    public Guid Id { get; set; }
    
    public string? Title { get; set; }
    
    public string? Description { get; set; }
    
    public decimal Price { get; set; }
    
    public byte[]? Image { get; set; }
    
    public int CategoryId { get; set; }
    
    public string? CategoryName { get; set; }
}

public static class ProductResponseExtensions
{
    public static ProductResponse ToProductResponse(this Product product)
    {
        return new ProductResponse
        {
            Id = product.Id,
            Title = product.Title,
            Description = product.Description,
            Price = product.Price,
            Image = product.Image,
            CategoryId = product.CategoryId,
            CategoryName = product.Category?.Title
        };
    }
}