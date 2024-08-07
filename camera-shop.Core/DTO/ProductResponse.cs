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