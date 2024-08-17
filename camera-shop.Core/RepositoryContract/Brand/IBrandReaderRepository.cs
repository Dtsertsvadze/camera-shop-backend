using camera_shop.Core.Entities;

namespace camera_shop.Core.RepositoryContract.Brand;

public interface IBrandReaderRepository
{
    public Task<List<ProductBrand>> GetAllBrandsAsync();
}