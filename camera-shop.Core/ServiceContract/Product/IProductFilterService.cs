using camera_shop.Core.DTO.Product;

namespace camera_shop.Core.ServiceContract.Product;

public interface IProductFilterService
{
    public Task<List<ProductResponse>> GetFilteredProductsAsync(ProductFilter productFilter);
}