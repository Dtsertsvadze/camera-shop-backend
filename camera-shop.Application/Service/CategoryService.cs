using camera_shop.Core.DTO;
using camera_shop.Core.Entities;
using camera_shop.Core.RepositoryContract;
using camera_shop.Core.ServiceContract;

namespace camera_shop.Application.Service;

public class CategoryService : ICategoryReaderService
{
    private readonly ICategoryReaderRepository _categoryReaderRepository;
    
    public CategoryService(ICategoryReaderRepository categoryReaderRepository)
    {
        _categoryReaderRepository = categoryReaderRepository;
    }

    public async Task<List<CategoryResponse>> GetAllCategoriesHierarchyAsync()
    {
        var categories = await _categoryReaderRepository.GetAllCategoriesAsync();   
        var rootCategories = categories.Where(c => c.ParentCategoryId == 1).ToList();
        
        return rootCategories.Select(c => MapCategoryToDto(c, categories)).ToList(); 
    }
    
    private static CategoryResponse MapCategoryToDto(Category category, List<Category> allCategories)
    {
        return new CategoryResponse()
        {
            Id = category.Id,
            Title = category.Title,
            Subcategories = allCategories
                .Where(c => c.ParentCategoryId == category.Id)
                .Select(subCategory => MapCategoryToDto(subCategory, allCategories))
                .ToList()
        };
    }
}