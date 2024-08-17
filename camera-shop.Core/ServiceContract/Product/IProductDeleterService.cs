namespace camera_shop.Core.ServiceContract.Product;

public interface IProductDeleterService
{
    public Task<bool> DeleteAsync(Guid productId);
}