using camera_shop.Core.DTO;
using camera_shop.Core.Entities;
using camera_shop.Core.RepositoryContract;
using camera_shop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace camera_shop.Infrastructure.Repository;

public class ProductRepository : IProductWriterRepository, IProductReaderRepository
{
    private readonly ApplicationDbContext _context;
    
    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Product> CreateAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<List<Product>?> GetAllAsync()
    {
        var products = await _context.Products.ToListAsync();

        return products;
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

        return product;
    }
}