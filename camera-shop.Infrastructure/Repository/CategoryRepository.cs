namespace camera_shop.Infrastructure.Repository;
using Core.Entities;
using Core.RepositoryContract.Category;
using Data;
using Microsoft.EntityFrameworkCore;

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