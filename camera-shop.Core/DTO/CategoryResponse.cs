namespace camera_shop.Core.DTO;

public class CategoryResponse
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public List<CategoryResponse>? Subcategories { get; set; }
}