using camera_shop.Core.Entities;
using camera_shop.Core.RepositoryContract.Category;

namespace camera_shop.Application.Service;
using Core.ServiceContract.Image;
using Core.DTO.Product;
using Core.RepositoryContract.Product;
using Core.ServiceContract.Product;

public class ProductService : IProductReaderService, IProductWriterService, IProductUpdaterService,
    IProductDeleterService, IProductFilterService
{
    private readonly IProductReaderRepository _productReaderRepository;
    private readonly IProductWriterRepository _productWriterRepository;
    private readonly IProductDeleterRepository _productDeleterRepository;
    private readonly IProductUpdaterRepository _productUpdaterRepository;
    private readonly IImageService _imageService;
    private readonly ICategoryReaderRepository _categoryRepository;

    public ProductService
    (
        ICategoryReaderRepository categoryRepository,
        IProductReaderRepository productReaderRepository,
        IProductWriterRepository productWriterRepository,
        IImageService imageService,
        IProductUpdaterRepository productUpdaterRepository,
        IProductDeleterRepository productDeleterRepository
    )
    {
        _productReaderRepository = productReaderRepository;
        _productWriterRepository = productWriterRepository;
        _imageService = imageService;
        _productUpdaterRepository = productUpdaterRepository;
        _productDeleterRepository = productDeleterRepository;
        _categoryRepository = categoryRepository;
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

    public async Task<ProductResponse> CreateProductAsync(ProductAddRequest productAddRequest)
    {
        if (productAddRequest.Images == null || !productAddRequest.Images.Any())
        {
            throw new ArgumentException("At least one image is required.");
        }

        List<string> imageUrls = await _imageService.SaveImagesAsync(productAddRequest.Images);
        var product = productAddRequest.ToProduct(imageUrls);
        var createdProduct = await _productWriterRepository.CreateAsync(product);

        return createdProduct.ToProductResponse();
    }

    public async Task<ProductResponse> UpdateAsync(ProductUpdateRequest productUpdateRequest)
    {
        ArgumentNullException.ThrowIfNull(productUpdateRequest, nameof(productUpdateRequest));

        var existingProduct = await _productReaderRepository.GetByIdAsync(productUpdateRequest.Id);

        ArgumentNullException.ThrowIfNull(existingProduct, nameof(existingProduct));
        ArgumentNullException.ThrowIfNull(existingProduct.ImageUrls, nameof(existingProduct.ImageUrls));

        var imagesToDelete = existingProduct.ImageUrls.Except(productUpdateRequest.ImageUrls).ToList();
        if (imagesToDelete.Any())
        {
            await _imageService.DeleteImagesAsync(imagesToDelete);
        }

        var newImageUrls = new List<string>();
        if (productUpdateRequest.NewImages != null && productUpdateRequest.NewImages.Any())
        {
            newImageUrls = await _imageService.SaveImagesAsync(productUpdateRequest.NewImages);
        }

        var updatedImageUrls = productUpdateRequest.ImageUrls.Concat(newImageUrls).Distinct().ToList();

        existingProduct.Title = productUpdateRequest.Title;
        existingProduct.Description = productUpdateRequest.Description;
        existingProduct.Price = productUpdateRequest.Price;
        existingProduct.CategoryId = productUpdateRequest.CategoryId;
        existingProduct.ImageUrls = updatedImageUrls;

        var result = await _productUpdaterRepository.UpdateAsync(existingProduct);

        return result.ToProductResponse();
    }

    public Task<bool> DeleteAsync(Guid productId)
    {
        return _productDeleterRepository.DeleteAsync(productId);
    }

    public async Task<List<ProductResponse>> GetFilteredProductsAsync(ProductFilter productFilter)
    {
        var products = await _productReaderRepository.GetAllAsync();
        var categories = await _categoryRepository.GetAllCategoriesAsync();

        ArgumentNullException.ThrowIfNull(products, "No products found");

        if (productFilter.Categories.Count > 0)
        {
            var allRelevantCategoryIds = new HashSet<int>();
            foreach (var categoryId in productFilter.Categories)
            {
                allRelevantCategoryIds.Add(categoryId);
                AddChildCategories(categories, categoryId, allRelevantCategoryIds);
            }

            products = products.Where(p => allRelevantCategoryIds.Contains(p.CategoryId)).ToList();
        }

        if (productFilter.Brands.Count > 0)
        {
            products = products.Where(p => productFilter.Brands.Contains(p.BrandId)).ToList();
        }

        if (productFilter.MinPrice.HasValue)
        {
            products = products.Where(p => p.Price >= productFilter.MinPrice).ToList();
        }

        if (productFilter.MaxPrice.HasValue)
        {
            products = products.Where(p => p.Price <= productFilter.MaxPrice).ToList();
        }

        if (!string.IsNullOrWhiteSpace(productFilter.SortBy))
        {
            products = productFilter.SortBy switch
            {
                "asc" => products.OrderBy(p => p.Price).ToList(),
                "desc" => products.OrderByDescending(p => p.Price).ToList(),
                _ => products
            };
        }

        return products.Select(p => p.ToProductResponse()).ToList();
    }

    private void AddChildCategories(List<Category> allCategories, int parentId, HashSet<int> categoryIds)
    {
        var childCategories = allCategories.Where(c => c.ParentCategoryId == parentId);
        foreach (var child in childCategories)
        {
            categoryIds.Add(child.Id);
            AddChildCategories(allCategories, child.Id, categoryIds);
        }
    }
}