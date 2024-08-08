using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace camera_shop.Core.Validators;

public class MaxFileSizeAttribute(int maxFileSize) : ValidationAttribute
{

    protected override ValidationResult? IsValid(
        object? value, ValidationContext validationContext)
    {
        if (value is not IFormFile file) return ValidationResult.Success;
        return file.Length > maxFileSize ? new ValidationResult(GetErrorMessage()) : ValidationResult.Success;
    }

    private string GetErrorMessage()
    {
        return $"Maximum allowed file size is {maxFileSize} bytes.";
    }
}

public class AllowedExtensionsAttribute(string[] extensions) : ValidationAttribute
{

    protected override ValidationResult? IsValid(
        object? value, ValidationContext validationContext)
    {
        if (value is not IFormFile file) return ValidationResult.Success;
        var extension = Path.GetExtension(file.FileName);
        return !extensions.Contains(extension.ToLower()) ? new ValidationResult(GetErrorMessage()) : ValidationResult.Success;
    }

    private string GetErrorMessage()
    {
        return $"This file extension is not allowed. Allowed extensions are: {string.Join(", ", extensions)}";
    }
}
