using final_project.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace final_project.Data.Initialization;

public class DataInitializer
{
    private readonly RoleManager<Role> _roleManager;

    public DataInitializer(RoleManager<Role> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task Seed()
    {
        var adminRole = await _roleManager.FindByNameAsync("Admin");
        var userRole = await _roleManager.FindByNameAsync("User");

        if (adminRole is null)
        {
            await _roleManager.CreateAsync(new Role { Name = "Admin" });
            adminRole = await _roleManager.FindByNameAsync("Admin");
        }

        if (userRole is null)
        {
            await _roleManager.CreateAsync(new Role() { Name = "User" });
            userRole = await _roleManager.FindByNameAsync("User");
        }
    }
}