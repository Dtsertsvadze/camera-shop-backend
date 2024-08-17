using camera_shop.Core.DTO.Brand;
using camera_shop.Core.RepositoryContract.Brand;
using camera_shop.Core.ServiceContract.Brand;

namespace camera_shop.Application.Service;

public class BrandService : IBrandReaderService
{
    private readonly IBrandReaderRepository _brandReaderRepository;
    
    public BrandService(IBrandReaderRepository brandReaderRepository)
    {
        _brandReaderRepository = brandReaderRepository;
    }

    public async Task<List<BrandResponse>> GetAllBrandsAsync()
    {
        var brands = await _brandReaderRepository.GetAllBrandsAsync();
        
        ArgumentNullException.ThrowIfNull(brands, nameof(brands));
        
        return brands.Select(b => new BrandResponse()
        {
            Id = b.Id,
            Name = b.Name
        }).ToList();
    }
}