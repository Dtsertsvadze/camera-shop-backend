namespace camera_shop.Core.DTO.Product;
using Entities;

public class ProductResponse
{
    public Guid Id { get; set; }
    
    public string? Title { get; set; }
    
    public string? Description { get; set; }
    
    public decimal Price { get; set; }
    
    public List<string>? ImageUrls { get; set; }
    
    public int CategoryId { get; set; }
    
    public string? CategoryName { get; set; }

    public int BrandId { get; set; }
    
    public string? BrandName { get; set; }
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
            ImageUrls = product.ImageUrls,
            CategoryId = product.CategoryId,
            CategoryName = product.Category?.Title,
            BrandId = product.BrandId,
            BrandName = product.Brand?.Name
        };
    }
}