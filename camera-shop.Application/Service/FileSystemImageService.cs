using Microsoft.Extensions.Logging;

namespace camera_shop.Application.Service;
using camera_shop.Core.ServiceContract.Image;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

public class FileSystemImageService : IImageService
{
    private readonly string _imageDirectory;
    private readonly IWebHostEnvironment _environment;
    private readonly ILogger<FileSystemImageService> _logger;

    public FileSystemImageService(IWebHostEnvironment environment, ILogger<FileSystemImageService> logger)
    {
        _environment = environment;
        _imageDirectory = Path.Combine(_environment.WebRootPath, "images", "products");
        _logger = logger;
    }

    public async Task<List<string>> SaveImagesAsync(List<IFormFile> imageFiles)
    {
        if (imageFiles == null || imageFiles.Count == 0)
        {
            throw new ArgumentException("No files were uploaded");
        }

        if (!Directory.Exists(_imageDirectory))
        {
            Directory.CreateDirectory(_imageDirectory);
        }

        var savedImagePaths = new List<string>();

        foreach (var imageFile in imageFiles)
        {
            if (imageFile.Length == 0)
            {
                continue; 
            }

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
            var filePath = Path.Combine(_imageDirectory, fileName);

            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            savedImagePaths.Add($"/images/products/{fileName}");
        }

        if (savedImagePaths.Count == 0)
        {
            throw new InvalidOperationException("No valid images were uploaded");
        }

        return savedImagePaths;
    }

    public async Task DeleteImagesAsync(List<string>? imageUrls)
    {
        if (imageUrls == null || imageUrls.Count == 0)
        {
            return;
        }

        foreach (var imageUrl in imageUrls)
        {
            try
            {
                var imagePath = Path.Combine(_environment.WebRootPath, imageUrl.TrimStart('/'));
                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                    _logger.LogInformation($"Deleted image: {imagePath}");
                }
                else
                {
                    _logger.LogWarning($"Image not found for deletion: {imagePath}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting image: {imageUrl}");
            }
        }

        await Task.CompletedTask;
    }

}