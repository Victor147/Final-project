using AutoMapper;
using final_project.Data.Entities;
using final_project.Helpers;
using final_project.Models;
using final_project.Services.OrderService;
using final_project.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace final_project.Controllers;

public class ProfileController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;

    public ProfileController(UserManager<User> userManager, IOrderService orderService, IMapper mapper)
    {
        _userManager = userManager;
        _orderService = orderService;
        _mapper = mapper;
    }

    [Authorize(Roles = "User")]
    public async Task<IActionResult> ActiveUserOrders(int page = 1, int perPage = 8)
    {
        var username = HttpContext.User.Identity!.Name;
        var user = await _userManager.FindByNameAsync(username);

        var model = await _orderService.GetAllUnfinishedOrdersForUserAsync(user.Id);

        var orders = model.ToList()
            .Skip((page - 1) * perPage)
            .Take(perPage);
        
        ViewData["QueryParameters"] = new Dictionary<string, string>
        {
            { "Page", page.ToString() },
            { "PerPage", perPage.ToString() }
        };

        return View(new ReturnPaginatedOrdersViewModel()
        {
            Orders = orders,
            PaginationProperties = PaginationHelper.CalculateProperties(page, 
                model.Count, 
                perPage)
        });
        
    }
    
    [Authorize(Roles = "User")]
    public async Task<IActionResult> FinishedUserOrders(int page = 1, int perPage = 8)
    {
        var username = HttpContext.User.Identity!.Name;
        var user = await _userManager.FindByNameAsync(username);

        var model = await _orderService.GetAllFinishedOrdersForUserAsync(user.Id);
        
        var orders = model.ToList()
            .Skip((page - 1) * perPage)
            .Take(perPage);
        
        ViewData["QueryParameters"] = new Dictionary<string, string>
        {
            { "Page", page.ToString() },
            { "PerPage", perPage.ToString() }
        };

        return View(new ReturnPaginatedOrdersViewModel()
        {
            Orders = orders,
            PaginationProperties = PaginationHelper.CalculateProperties(page,
                model.Count,
                perPage)
        });
    }

    [Authorize(Roles = "User")]
    public async Task<IActionResult> Profile()
    {
        var username = HttpContext.User.Identity!.Name;
        var user = await _userManager.FindByNameAsync(username);

        var model = _mapper.Map<ProfileModel>(user);
        
        return View(model);
    }
}