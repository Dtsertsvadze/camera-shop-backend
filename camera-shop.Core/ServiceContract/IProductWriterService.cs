using camera_shop.Core.DTO;
using Microsoft.AspNetCore.Http;

namespace camera_shop.Core.ServiceContract;

public interface IProductWriterService
{
    Task<ProductResponse> CreateProductAsync(ProductAddRequest productRequest, IFormFile imageFile);
}