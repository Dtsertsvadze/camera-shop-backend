namespace camera_shop.Core.DTO.Product;
using System.ComponentModel.DataAnnotations;
using Validators;
using Microsoft.AspNetCore.Http;
using Entities;

public class ProductUpdateRequest
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    [StringLength(40, ErrorMessage = "Title cannot be longer than 40 characters.")]
    public string? Title { get; set; }
    
    [Required]
    [StringLength(200, ErrorMessage = "Description cannot be longer than 200 characters.")]
    public string? Description { get; set; }
    
    [Required]
    [Range(0.01, 100000, ErrorMessage = "Price must be between 0.01 and 100,000.")]
    public decimal Price { get; set; }
    
    [Required]
    public int CategoryId { get; set; }
    
    [Required]
    public int BrandId { get; set; }

    [Required]
    public List<string> ImageUrls { get; set; }  = new List<string>();

    [MaxFileCount(8, ErrorMessage = "You can upload a maximum of 8 images.")]
    public List<IFormFile>? NewImages { get; set; }
}

public static class ProductUpdateRequestExtensions
{
    public static Product ToProduct(this ProductUpdateRequest productRequest, List<string> imageUrls)
    {
        return new Product
        {
            Id = productRequest.Id,
            Title = productRequest.Title,
            Description = productRequest.Description,
            Price = productRequest.Price,
            CategoryId = productRequest.CategoryId,
            BrandId = productRequest.BrandId,
            ImageUrls = imageUrls
        };
    }
}