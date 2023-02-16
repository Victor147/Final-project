using final_project.Data.Initialization;
using final_project.Data.Persistence;
using final_project.Helpers.Cloudinary;
using final_project.Models;
using final_project.Services.ProductService;

namespace final_project.Extensions;

public static class GlobalExtensions
{
    public static void AddDependencyInjection(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<EcommerceDbContext, EcommerceDbContext>();
        builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddTransient<DataInitializer, DataInitializer>();
        builder.Services.AddTransient<IImageUploader, ImageUploader>();
        builder.Services.AddTransient<IProductService, ProductService>();
    }
}