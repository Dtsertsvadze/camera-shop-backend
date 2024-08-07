using camera_shop.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace camera_shop.Infrastructure.Data;

public static class ModelBuilderExtensions
{
    public static void SeedCategories(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Title = "Camera Equipment", ParentCategoryId = null },
            new Category { Id = 2, Title = "Cameras", ParentCategoryId = 1 },
            new Category { Id = 3, Title = "Lenses", ParentCategoryId = 1 },
            new Category { Id = 4, Title = "Accessories", ParentCategoryId = 1 },
            new Category { Id = 5, Title = "DSLR", ParentCategoryId = 2 },
            new Category { Id = 6, Title = "Mirrorless", ParentCategoryId = 2 },
            new Category { Id = 7, Title = "Point-and-Shoot", ParentCategoryId = 2 },
            new Category { Id = 8, Title = "Prime Lenses", ParentCategoryId = 3 },
            new Category { Id = 9, Title = "Zoom Lenses", ParentCategoryId = 3 },
            new Category { Id = 10, Title = "Wide-Angle Lenses", ParentCategoryId = 3 },
            new Category { Id = 11, Title = "Camera Bags", ParentCategoryId = 4 },
            new Category { Id = 12, Title = "Tripods", ParentCategoryId = 4 },
            new Category { Id = 13, Title = "Memory Cards", ParentCategoryId = 4 }
        );
    }
}