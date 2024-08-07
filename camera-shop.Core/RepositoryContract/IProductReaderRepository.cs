using camera_shop.Core.DTO;
using camera_shop.Core.Entities;

namespace camera_shop.Core.RepositoryContract;

public interface IProductReaderRepository
{
    public Task<List<Product>?> GetAllAsync();
    
    public Task<Product?> GetByIdAsync(Guid id);
}