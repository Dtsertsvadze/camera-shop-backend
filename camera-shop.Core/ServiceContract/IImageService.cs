using Microsoft.AspNetCore.Http;

namespace camera_shop.Core.ServiceContract;

public interface IImageService
{
    Task<string> SaveImageAsync(IFormFile imageFile);
}