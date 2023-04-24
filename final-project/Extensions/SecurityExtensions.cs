using final_project.Data.Entities;
using final_project.Data.Persistence;
using Microsoft.AspNetCore.Identity;

namespace final_project.Extensions;

public static class SecurityExtensions
{
    public static void AddSecurity(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        services.AddIdentity<User, Role>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
            options.Password.RequiredUniqueChars = 0;
        }).AddEntityFrameworkStores<EcommerceDbContext>();
    }

    public static void UseSecurity(this WebApplication application)
    {
        application.UseAuthentication();
        application.UseAuthorization();
        application.UseHttpsRedirection();
    }
}