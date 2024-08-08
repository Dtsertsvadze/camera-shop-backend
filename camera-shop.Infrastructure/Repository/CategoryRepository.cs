using camera_shop.Core.Entities;
using camera_shop.Core.RepositoryContract;
using camera_shop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace camera_shop.Infrastructure.Repository;

public class CategoryRepository : ICategoryReaderRepository
{
    private readonly ApplicationDbContext _context;
    
    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        return await _context.Categories.ToListAsync();
    }
}