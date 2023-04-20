using final_project.Data.Initialization;
using final_project.Data.Persistence;
using final_project.Services.CategoryService;
using final_project.Services.Cloudinary;
using final_project.Services.ManufacturerService;
using final_project.Services.OrderDetailService;
using final_project.Services.OrderService;
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
        builder.Services.AddTransient<IManufacturerService, ManufacturerService>();
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddTransient<IOrderService, OrderService>();
        builder.Services.AddTransient<IOrderDetailService, OrderDetailService>();
        builder.Services.AddTransient<ICategoryService, CategoryService>();
    }
}