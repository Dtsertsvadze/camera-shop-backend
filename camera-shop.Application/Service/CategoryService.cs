namespace camera_shop.Application.Service;
using Core.DTO.Category;
using Core.Entities;
using Core.RepositoryContract.Category;
using Core.ServiceContract.Category;

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