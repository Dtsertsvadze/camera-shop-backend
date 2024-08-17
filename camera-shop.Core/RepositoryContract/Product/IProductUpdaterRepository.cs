namespace camera_shop.Core.RepositoryContract.Product;
using Entities;

public interface IProductUpdaterRepository
{
    public Task<Product> UpdateAsync(Product product);
}