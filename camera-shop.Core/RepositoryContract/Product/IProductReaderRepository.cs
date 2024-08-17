namespace camera_shop.Core.RepositoryContract.Product;
using Entities;


public interface IProductReaderRepository
{
    public Task<List<Product>?> GetAllAsync();
    
    public Task<Product?> GetByIdAsync(Guid id);
}