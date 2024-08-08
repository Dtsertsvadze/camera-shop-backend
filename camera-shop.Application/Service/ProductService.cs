using camera_shop.Core.DTO;
using camera_shop.Core.RepositoryContract;
using camera_shop.Core.ServiceContract;
using Microsoft.AspNetCore.Http;

namespace camera_shop.Application.Service;

public class ProductService : IProductReaderService, IProductWriterService
{
    private readonly IProductReaderRepository _productReaderRepository;
    private readonly IProductWriterRepository _productWriterRepository;
    private readonly IImageService _imageService;
    
    public ProductService(IProductReaderRepository productReaderRepository, IProductWriterRepository productWriterRepository, IImageService imageService)
    {
        _productReaderRepository = productReaderRepository;
        _productWriterRepository = productWriterRepository;
        _imageService = imageService;
    }

    public async Task<List<ProductResponse>> GetAllProductsAsync()
    {
        var products = await _productReaderRepository.GetAllAsync();
        
        ArgumentNullException.ThrowIfNull(products, "No products found");

        var productResponses = products.Select(p => p.ToProductResponse()).ToList();
        
        return productResponses;
    }

    public async Task<ProductResponse?> GetProductByIdAsync(Guid id)
    {
        var product = await _productReaderRepository.GetByIdAsync(id);
        
        ArgumentNullException.ThrowIfNull(product, "Product not found");
        
        return product.ToProductResponse();
    }

    public async Task<ProductResponse> CreateProductAsync(ProductAddRequest productRequest, IFormFile imageFile)
    {
        string imageUrl = await _imageService.SaveImageAsync(imageFile);
        var product = productRequest.ToProduct(imageUrl);
        var createdProduct = await _productWriterRepository.CreateAsync(product);
        return createdProduct.ToProductResponse();
    }
}