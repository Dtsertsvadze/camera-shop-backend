namespace camera_shop.Infrastructure.Repository;
using Core.Entities;
using Core.RepositoryContract.Product;
using Data;
using Microsoft.EntityFrameworkCore;

public class ProductRepository : IProductWriterRepository, IProductReaderRepository, IProductUpdaterRepository, IProductDeleterRepository
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
        var products = await _context.Products
            .Include("Category")
            .Include("Brand")
            .ToListAsync();

        return products;
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        var product = await _context.Products
            .Include("Category")
            .Include("Brand")
            .FirstOrDefaultAsync(p => p.Id == id);

        return product;
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        var existingProduct = await _context.Products
            .Include("Category")
            .Include("Brand")
            .FirstOrDefaultAsync(p => p.Id == product.Id);
        
        _context.Products.Update(existingProduct);
        
        await _context.SaveChangesAsync();
        
        return product;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
        {
            return false;
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return true;
    }
}