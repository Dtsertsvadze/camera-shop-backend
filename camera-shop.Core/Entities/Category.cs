using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace camera_shop.Core.Entities;

public class Category
{
    [Key]
    public int Id { get; set; }
    
    public string? Title { get; set; }
    
    public int? ParentCategoryId { get; set; }
    
    [ForeignKey("ParentCategoryId")]
    public Category ParentCategory { get; set; } = null!;
    
    public ICollection<Category> Subcategories { get; set; } = new List<Category>();
    
    public ICollection<Product> Products { get; set; } = new List<Product>();
}