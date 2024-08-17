namespace camera_shop.Core.ServiceContract.Product;
using camera_shop.Core.DTO.Product;

public interface IProductWriterService
{
    Task<ProductResponse> CreateProductAsync(ProductAddRequest productAddRequest);
}