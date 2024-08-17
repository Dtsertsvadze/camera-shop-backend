namespace camera_shop.Core.ServiceContract.Product;
using camera_shop.Core.DTO.Product;

public interface IProductUpdaterService
{
    public Task<ProductResponse> UpdateAsync(ProductUpdateRequest productUpdateRequest);
}