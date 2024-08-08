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
    private readonly IImageService _imageService;
    
    public ProductsController(IProductReaderService productReaderService, IProductWriterService productWriterService, IImageService imageService)
    {
        _productReaderService = productReaderService;
        _productWriterService = productWriterService;
        _imageService = imageService;
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
    [Route("[action]")]
    public async Task<IActionResult> CreateProduct([FromForm] ProductAddRequest productAddRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var createdProduct = await _productWriterService.CreateProductAsync(productAddRequest, productAddRequest.Image);
            return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);
        }
        catch (Exception ex) when (ex is ArgumentNullException or ArgumentException)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "An unexpected error occurred while creating the product.");
        }
    }
}