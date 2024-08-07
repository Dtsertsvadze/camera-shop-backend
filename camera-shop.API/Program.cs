using camera_shop.Application.Service;
using camera_shop.Core.RepositoryContract;
using camera_shop.Core.ServiceContract;
using camera_shop.Infrastructure.Data;
using camera_shop.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IProductReaderRepository, ProductRepository>();
builder.Services.AddScoped<IProductWriterRepository, ProductRepository>();
builder.Services.AddScoped<IProductReaderService, ProductService>();
builder.Services.AddScoped<IProductWriterService, ProductService>();

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
app.MapControllers();

app.Run();