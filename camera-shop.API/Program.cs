using camera_shop.Application.Service;
using camera_shop.Core.RepositoryContract.Brand;
using camera_shop.Core.RepositoryContract.Category;
using camera_shop.Core.RepositoryContract.Product;
using camera_shop.Core.ServiceContract.Brand;
using camera_shop.Core.ServiceContract.Image;
using camera_shop.Core.ServiceContract.Category;
using camera_shop.Core.ServiceContract.Product;
using camera_shop.Infrastructure.Data;
using camera_shop.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:5555")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Repositories
// Product
builder.Services.AddScoped<IProductReaderRepository, ProductRepository>();
builder.Services.AddScoped<IProductWriterRepository, ProductRepository>();
builder.Services.AddScoped<IProductDeleterRepository, ProductRepository>();
builder.Services.AddScoped<IProductUpdaterRepository, ProductRepository>();

// Category
builder.Services.AddScoped<ICategoryReaderService, CategoryService>();
builder.Services.AddScoped<IBrandReaderService, BrandService>();

// Services
// Product
builder.Services.AddScoped<IProductReaderService, ProductService>();
builder.Services.AddScoped<IProductWriterService, ProductService>();
builder.Services.AddScoped<IProductUpdaterService, ProductService>();
builder.Services.AddScoped<IProductDeleterService, ProductService>();
builder.Services.AddScoped<IProductFilterService, ProductService>();

// Category
builder.Services.AddScoped<ICategoryReaderRepository, CategoryRepository>();
builder.Services.AddScoped<IBrandReaderRepository, BrandRepository>();

// Image
builder.Services.AddSingleton<IImageService, FileSystemImageService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseStaticFiles();
app.UseRouting();
app.UseCors("AllowReactApp");
app.MapControllers();

app.Run();