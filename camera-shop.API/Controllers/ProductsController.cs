namespace camera_shop.API.Controllers;
using Core.DTO.Product;
using Core.ServiceContract.Image;
using Core.ServiceContract.Product;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductReaderService _productReaderService;
    private readonly IProductWriterService _productWriterService;
    private readonly IProductUpdaterService _productUpdaterService;
    private readonly IProductDeleterService _productDeleterService;
    private readonly IProductFilterService _productFilterService;
    public ProductsController(
        IProductReaderService productReaderService,
        IProductWriterService productWriterService,
        IProductUpdaterService productUpdaterService,
        IProductDeleterService productDeleterService,
        IProductFilterService productFilterService)
    {
        _productUpdaterService = productUpdaterService;
        _productReaderService = productReaderService;
        _productWriterService = productWriterService;
        _productDeleterService = productDeleterService;
        _productFilterService = productFilterService;
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
    
    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetFilteredProducts(
        [FromQuery] List<int>? brands,
        [FromQuery] List<int>? categories,
        [FromQuery] decimal? minPrice,
        [FromQuery] decimal? maxPrice,
        [FromQuery] string? sortBy)
    {
        var productFilterRequest = new ProductFilter
        {
            Brands = brands ?? new List<int>(),
            Categories = categories ?? new List<int>(),
            MinPrice = minPrice,
            MaxPrice = maxPrice,
            SortBy = sortBy
        };

        try
        {
            var products = await _productFilterService.GetFilteredProductsAsync(productFilterRequest);
            return Ok(products);
        }
        catch (Exception ex)
        {
            return BadRequest($"An error occurred: {ex.Message}");
        }
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
            var createdProduct = await _productWriterService.CreateProductAsync(productAddRequest);
            return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);
        }
        catch (Exception ex) when (ex is ArgumentNullException or ArgumentException)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }
    
    [HttpPut]
    [Route("[action]")]
    public async Task<IActionResult> UpdateProduct([FromForm] ProductUpdateRequest productUpdateRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var updatedProduct = await _productUpdaterService.UpdateAsync(productUpdateRequest);
            return Ok(updatedProduct);
        }
        catch (Exception ex) when (ex is ArgumentNullException or ArgumentException)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }
    
    [HttpDelete]
    [Route("[action]/{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        try
        {
            var isDeleted = await _productDeleterService.DeleteAsync(id);
            if (isDeleted)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }
}