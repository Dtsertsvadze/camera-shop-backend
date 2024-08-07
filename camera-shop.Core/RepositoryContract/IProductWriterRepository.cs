using camera_shop.Core.Entities;

namespace camera_shop.Core.RepositoryContract;

public interface IProductWriterRepository
{
    public Task<Product> CreateAsync(Product product);
}