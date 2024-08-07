using camera_shop.Core.DTO;
using camera_shop.Core.ServiceContract;
using Microsoft.AspNetCore.Mvc;

namespace camera_shop.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductReaderService _productReaderService;
    private readonly IProductWriterService _productWriterService;
    
    public ProductsController(IProductReaderService productReaderService, IProductWriterService productWriterService)
    {
        _productReaderService = productReaderService;
        _productWriterService = productWriterService;
    }
    
    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _productReaderService.GetAllProductsAsync();
        
        return Ok(products);
    }
    
    [HttpGet]
    [Route("[action]/{id}")]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        var product = await _productReaderService.GetProductByIdAsync(id);
        
        return Ok(product);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] ProductAddRequest productAddRequest)
    {
        var createdProduct = await _productWriterService.CreateProductAsync(productAddRequest);
        
        return Ok(createdProduct);
    }
}