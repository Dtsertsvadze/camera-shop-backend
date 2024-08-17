namespace camera_shop.Core.RepositoryContract.Product;
using Entities;

public interface IProductWriterRepository
{
    public Task<Product> CreateAsync(Product product);
}