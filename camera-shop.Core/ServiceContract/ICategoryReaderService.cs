using camera_shop.Core.DTO;

namespace camera_shop.Core.ServiceContract;

public interface ICategoryReaderService
{
    public Task<List<CategoryResponse>> GetAllCategoriesHierarchyAsync();
}