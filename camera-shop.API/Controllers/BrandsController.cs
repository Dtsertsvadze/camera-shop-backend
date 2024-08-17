using camera_shop.Core.ServiceContract.Brand;
using Microsoft.AspNetCore.Mvc;

namespace camera_shop.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BrandsController : ControllerBase
{
    private readonly IBrandReaderService _brandReaderService;
    
    public BrandsController(IBrandReaderService brandReaderService)
    {
        _brandReaderService = brandReaderService;
    }
    
    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetAllBrands()
    {
        var brands = await _brandReaderService.GetAllBrandsAsync();
        
        return Ok(brands);
    }
}