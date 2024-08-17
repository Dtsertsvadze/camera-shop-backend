namespace camera_shop.Core.ServiceContract.Product;
using camera_shop.Core.DTO.Product;

public interface IProductReaderService
{
    public Task<List<ProductResponse>> GetAllProductsAsync();

    public Task<ProductResponse?> GetProductByIdAsync(Guid id);
}