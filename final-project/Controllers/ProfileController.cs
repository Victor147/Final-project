using final_project.Data.Entities;
using final_project.Services.OrderService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace final_project.Controllers;

public class ProfileController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IOrderService _orderService;

    public ProfileController(UserManager<User> userManager, IOrderService orderService)
    {
        _userManager = userManager;
        _orderService = orderService;
    }
    
    [Authorize(Roles = "User")]
    public async Task<IActionResult> UserOrders()
    {
        var username = HttpContext.User.Identity!.Name;
        var user = await _userManager.FindByNameAsync(username);

        var orders = await _orderService.GetAllOrdersForUserAsync(user.Id);
        
        return View(orders);
    }
}