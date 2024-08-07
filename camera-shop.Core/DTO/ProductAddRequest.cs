using System.ComponentModel.DataAnnotations;
using camera_shop.Core.Entities;

namespace camera_shop.Core.DTO;

public class ProductAddRequest
{
    [Required]
    [StringLength(40)]
    public string? Title { get; set; }
    
    [Required]
    [StringLength(200)]
    public string? Description { get; set; }
    
    [Required]
    [Range(0.01, 100000)]
    public decimal Price { get; set; }
    
    [Required]
    public byte[]? Image { get; set; }
    
    public int CategoryId { get; set; }
}

public static class ProductAddRequestExtensions
{
    public static Product ToProduct(this ProductAddRequest productRequest)
    {
        return new Product
        {
            Title = productRequest.Title,
            Description = productRequest.Description,
            Price = productRequest.Price,
            Image = productRequest.Image,
            CategoryId = productRequest.CategoryId
        };
    }
}