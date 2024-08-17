namespace camera_shop.Core.DTO.Product;

public class ProductFilter
{
    public List<int> Brands { get; set; } = new List<int>();
    public List<int> Categories { get; set; } = new List<int>();
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public string? SortBy { get; set; }
}