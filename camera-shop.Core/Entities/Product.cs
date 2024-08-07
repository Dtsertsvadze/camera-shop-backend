using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace camera_shop.Core.Entities;

public class Product
{
    [Key]
    public Guid Id { get; set; }
    
    public string? Title { get; set; }
    
    public string? Description { get; set; }
    
    public decimal Price { get; set; }
    
    public byte[]? Image { get; set; }
    
    public int CategoryId { get; set; }
    
    [ForeignKey("CategoryId")]
    public Category? Category { get; set; }
}