using final_project.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace final_project.Data.Initialization;

public class DataInitializer
{
    private readonly RoleManager<Role> _roleManager;
    private readonly UserManager<User> _userManager;

    public DataInitializer(RoleManager<Role> roleManager, UserManager<User> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
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
            await _userManager.CreateAsync(new User { UserName = "admin" }, "admin");
            admin = await _userManager.FindByNameAsync("admin");
            await _userManager.AddToRoleAsync(admin, "admin");
        }

        if (user is null)
        {
            await _userManager.CreateAsync(new User { UserName = "user" }, "user");
            user = await _userManager.FindByNameAsync("user");
            await _userManager.AddToRoleAsync(user, "user");
        }
    }
}