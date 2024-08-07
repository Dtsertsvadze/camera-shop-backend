using camera_shop.Core.DTO;
using camera_shop.Core.RepositoryContract;
using camera_shop.Core.ServiceContract;

namespace camera_shop.Application.Service;

public class ProductService : IProductReaderService, IProductWriterService
{
    private readonly IProductReaderRepository _productReaderRepository;
    private readonly IProductWriterRepository _productWriterRepository;
    
    public ProductService(IProductReaderRepository productReaderRepository, IProductWriterRepository productWriterRepository)
    {
        _productReaderRepository = productReaderRepository;
        _productWriterRepository = productWriterRepository;
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

    public async Task<ProductResponse> CreateProductAsync(ProductAddRequest productRequest)
    {
        var product = productRequest.ToProduct();
        
        product.Id = Guid.NewGuid();
        
        var createdProduct = await _productWriterRepository.CreateAsync(product);
        
        ArgumentNullException.ThrowIfNull(createdProduct, "Product not created");
        
        return createdProduct.ToProductResponse();
    }
}