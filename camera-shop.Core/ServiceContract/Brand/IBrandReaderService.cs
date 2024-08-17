using camera_shop.Core.DTO.Brand;

namespace camera_shop.Core.ServiceContract.Brand;

public interface IBrandReaderService
{
    public Task<List<BrandResponse>> GetAllBrandsAsync();
}