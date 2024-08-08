using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace camera_shop.Core.Entities;

public class Product
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    [StringLength(40)]
    public string? Title { get; set; }
    
    [Required]
    [StringLength(200)]
    public string? Description { get; set; }
    
    [Required]
    [Range(0.01, 100000)]
    public decimal Price { get; set; }
    
    public string? ImageUrl { get; set; }
    
    public int CategoryId { get; set; }
    
    [ForeignKey("CategoryId")]
    public Category? Category { get; set; }
}