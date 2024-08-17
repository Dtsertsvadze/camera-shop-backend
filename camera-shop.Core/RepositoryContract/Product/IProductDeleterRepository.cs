namespace camera_shop.Core.RepositoryContract.Product;

public interface IProductDeleterRepository
{
    public Task<bool> DeleteAsync(Guid id);
}