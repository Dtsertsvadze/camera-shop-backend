using camera_shop.Core.ServiceContract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace camera_shop.Application.Service;

public class FileSystemImageService : IImageService
{
    private readonly string _imageDirectory;
    private readonly IWebHostEnvironment _environment;

    public FileSystemImageService(IWebHostEnvironment environment)
    {
        _environment = environment;
        _imageDirectory = Path.Combine(_environment.WebRootPath, "images", "products");
    }

    public async Task<string> SaveImageAsync(IFormFile imageFile)
    {
        if (imageFile == null || imageFile.Length == 0)
        {
            throw new ArgumentException("Invalid file");
        }

        if (!Directory.Exists(_imageDirectory))
        {
            Directory.CreateDirectory(_imageDirectory);
        }

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
        var filePath = Path.Combine(_imageDirectory, fileName);

        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await imageFile.CopyToAsync(stream);
        }

        return $"/images/products/{fileName}";
    }
}