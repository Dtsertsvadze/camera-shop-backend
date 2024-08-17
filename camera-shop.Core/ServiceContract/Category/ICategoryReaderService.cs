namespace camera_shop.Core.ServiceContract.Category;
using camera_shop.Core.DTO.Category;

public interface ICategoryReaderService
{
    public Task<List<CategoryResponse>> GetAllCategoriesHierarchyAsync();
}