using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace camera_shop.Core.Validators;

public class MaxFileCountAttribute : ValidationAttribute
{
    private readonly int _maxFileCount;

    public MaxFileCountAttribute(int maxFileCount)
    {
        _maxFileCount = maxFileCount;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is List<IFormFile> files)
        {
            if (files.Count > _maxFileCount)
            {
                return new ValidationResult($"You can upload a maximum of {_maxFileCount} files.");
            }
        }
        return ValidationResult.Success;
    }
}
