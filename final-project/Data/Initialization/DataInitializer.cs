using final_project.Data.Entities;
using final_project.Models;
using final_project.Services.CategoryService;
using final_project.Services.ManufacturerService;
using final_project.Services.ProductService;
using Microsoft.AspNetCore.Identity;

namespace final_project.Data.Initialization;

public class DataInitializer
{
    private readonly RoleManager<Role> _roleManager;
    private readonly UserManager<User> _userManager;
    private readonly IManufacturerService _manufacturerService;
    private readonly  IProductService _productService;
    private readonly ICategoryService _categoryService;

    public DataInitializer(RoleManager<Role> roleManager, UserManager<User> userManager, IManufacturerService manufacturerService, IProductService productService, ICategoryService categoryService)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _manufacturerService = manufacturerService;
        _productService = productService;
        _categoryService = categoryService;
    }

    public async Task Seed()
    {
        var adminRole = await _roleManager.FindByNameAsync("Admin");
        var userRole = await _roleManager.FindByNameAsync("User");

        var admin = await _userManager.FindByNameAsync("admin");
        var user = await _userManager.FindByNameAsync("user");

        if (adminRole is null)
        {
            await _roleManager.CreateAsync(new Role { Name = "Admin" });
            adminRole = await _roleManager.FindByNameAsync("Admin");
        }

        if (userRole is null)
        {
            await _roleManager.CreateAsync(new Role { Name = "User" });
            userRole = await _roleManager.FindByNameAsync("User");
        }

        if (admin is null)
        {
            await _userManager.CreateAsync(new User { UserName = "admin" }, "Admin123");
            admin = await _userManager.FindByNameAsync("admin");
            await _userManager.AddToRoleAsync(admin, "admin");
        }

        if (user is null)
        {
            await _userManager.CreateAsync(new User { UserName = "user" }, "User123");
            user = await _userManager.FindByNameAsync("user");
            await _userManager.AddToRoleAsync(user, "user");
        }

        var categorySnooker = "Снукър";
        Category? testCategory1 = await _categoryService.ReadCategoryByNameAsync(categorySnooker);
        if (testCategory1 is null)
        {
            await _categoryService.CreateCategoryAsync(new CategoryModel
            {
                Name = categorySnooker
            });
        }
        
        var categoryBilliard = "Билярд";
        Category? testCategory2 = await _categoryService.ReadCategoryByNameAsync(categoryBilliard);
        if (testCategory2 is null)
        {
            await _categoryService.CreateCategoryAsync(new CategoryModel
            {
                Name = categoryBilliard
            });
        }
    }
}