using camera_shop.Core.Entities;
using camera_shop.Core.RepositoryContract.Brand;
using camera_shop.Core.ServiceContract.Brand;
using camera_shop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace camera_shop.Infrastructure.Repository;

public class BrandRepository : IBrandReaderRepository
{
    private readonly ApplicationDbContext _context;
    
    public BrandRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProductBrand>> GetAllBrandsAsync()
    {
        return await _context.Brands.ToListAsync();
    }

    public Task<ProductBrand> GetBrandByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}