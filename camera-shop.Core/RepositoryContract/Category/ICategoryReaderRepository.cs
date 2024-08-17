namespace camera_shop.Core.RepositoryContract.Category;
using Entities;

public interface ICategoryReaderRepository
{
    public Task<List<Category>> GetAllCategoriesAsync();
}