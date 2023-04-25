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

        for (int i = 1; i <= 5; i++)
        {
            var manufacturerName = "test" + i;
            Manufacturer? testManufacturer = await _manufacturerService.ReadManufacturerByNameAsync(manufacturerName);
            if (testManufacturer is null)
            {
                await _manufacturerService.CreateManufacturerAsync(new ManufacturerModel
                {
                    Name = manufacturerName
                });
            }
        }
        
        for (int i = 1; i <= 5; i++)
        {
            var categoryName = "test_category_" + i;
            Category? testCategory = await _categoryService.ReadCategoryByNameAsync(categoryName);
            if (testCategory is null)
            {
                await _categoryService.CreateCategoryAsync(new CategoryModel
                {
                    Name = categoryName
                });
            }
        }

        for (int i = 1; i <= 50; i++)
        {
            var productName = "test" + i;
            Product? testProduct = await _productService.ReadProductByNameAsync(productName);
            if (testProduct is null)
            {
                string path = @"wwwroot\img\background.jpg";
                string name = Path.GetFileName(path);
                byte[] bytes = File.ReadAllBytes(path);
                await _productService.CreateProductAsync(new ProductModel
                {
                    Name = productName,
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec cursus nisl vel arcu vestibulum vehicula. Fusce euismod quis dolor non efficitur.",
                    Stock = new Random().Next(1, 51),
                    Price = new Random().Next(10, 10000),
                    Image = new FormFile(new MemoryStream(bytes), 0, bytes.Length, name, name),
                    ManufacturerId = new Random().Next(1, 6),
                    CategoryId = new Random().Next(1, 6)
                });
            }
        }
    }
}