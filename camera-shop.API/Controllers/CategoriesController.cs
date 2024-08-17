using camera_shop.Core.ServiceContract.Category;
using Microsoft.AspNetCore.Mvc;

namespace camera_shop.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryReaderService _categoryReaderService;
    
    public CategoriesController(ICategoryReaderService categoryReaderService)
    {
        _categoryReaderService = categoryReaderService;
    }
    
    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetAllCategories()
    {
        var categories = await _categoryReaderService.GetAllCategoriesHierarchyAsync();
        
        return Ok(categories);
    }
}