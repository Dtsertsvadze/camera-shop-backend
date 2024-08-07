using camera_shop.Core.DTO;

namespace camera_shop.Core.ServiceContract;

public interface IProductWriterService
{
    public Task<ProductResponse> CreateProductAsync(ProductAddRequest productRequest);
}