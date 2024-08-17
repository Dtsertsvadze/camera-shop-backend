namespace camera_shop.Core.ServiceContract.Image;
using Microsoft.AspNetCore.Http;

public interface IImageService
{
    public Task<List<string>> SaveImagesAsync(List<IFormFile> imageFiles);
    
    public Task DeleteImagesAsync(List<string>? imageUrls);
}