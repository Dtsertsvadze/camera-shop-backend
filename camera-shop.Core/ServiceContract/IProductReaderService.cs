using camera_shop.Core.DTO;

namespace camera_shop.Core.ServiceContract;

public interface IProductReaderService
{
    public Task<List<ProductResponse>> GetAllProductsAsync();

    public Task<ProductResponse?> GetProductByIdAsync(Guid id);
}