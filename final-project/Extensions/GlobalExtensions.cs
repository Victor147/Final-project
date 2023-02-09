using final_project.Data.Initialization;
using final_project.Data.Persistence;

namespace final_project.Extensions;

public static class GlobalExtensions
{
    public static void AddDependencyInjection(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<EcommerceDbContext, EcommerceDbContext>();
        builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddTransient<DataInitializer, DataInitializer>();
    }
}