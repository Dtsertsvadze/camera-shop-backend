using System;
using System.ComponentModel.DataAnnotations;
using camera_shop.Core.Entities;
using camera_shop.Core.Validators;
using Microsoft.AspNetCore.Http;

namespace camera_shop.Core.DTO;

public class ProductAddRequest
{
    [Required]
    [StringLength(40, ErrorMessage = "Title cannot be longer than 40 characters.")]
    public string? Title { get; set; }
    
    [Required]
    [StringLength(200, ErrorMessage = "Description cannot be longer than 200 characters.")]
    public string? Description { get; set; }
    
    [Required]
    [Range(0.01, 100000, ErrorMessage = "Price must be between 0.01 and 100,000.")]
    public decimal Price { get; set; }
    
    [Required(ErrorMessage = "An image is required.")]
    [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png", ".gif" })]
    [MaxFileSize(5 * 1024 * 1024)]
    public IFormFile? Image { get; set; }
    
    [Required]
    public int CategoryId { get; set; }
}

public static class ProductAddRequestExtensions
{
    public static Product ToProduct(this ProductAddRequest productRequest, string imageUrl)
    {
        return new Product
        {
            Title = productRequest.Title,
            Description = productRequest.Description,
            Price = productRequest.Price,
            CategoryId = productRequest.CategoryId,
            ImageUrl = imageUrl
        };
    }
}