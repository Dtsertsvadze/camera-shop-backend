using camera_shop.Core.Entities;

namespace camera_shop.Core.RepositoryContract;

public interface ICategoryReaderRepository
{
    public Task<List<Category>> GetAllCategoriesAsync();
}