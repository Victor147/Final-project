using AutoMapper;
using final_project.Helpers;
using final_project.Services.OrderDetailService;
using final_project.Services.OrderService;
using final_project.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace final_project.Controllers;

public class AdminController : Controller
{
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;
    private readonly IOrderDetailService _orderDetailsService;

    public AdminController(IOrderService orderService, IMapper mapper, IOrderDetailService orderDetailsService)
    {
        _orderService = orderService;
        _mapper = mapper;
        _orderDetailsService = orderDetailsService;
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Panel()
    {
        return View();
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Orders()
    {
        return View();
    }

    [Authorize(Roles = "Admin")]
    public IActionResult PaymentOrders()
    {
        return View();
    }

    [Authorize(Roles = "Admin")]
    public IActionResult StatusOrders()
    {
        return View();
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> PaidOrders(int page = 1, int perPage = 8)
    {
        var model = await _orderService.GetAllPaidOrdersAsync();

        var orders = model.ToList()
            .Skip((page - 1) * perPage)
            .Take(perPage);

        ViewData["QueryParameters"] = new Dictionary<string, string>
        {
            { "Page", page.ToString() },
            { "PerPage", perPage.ToString() }
        };

        return View(new ReturnPaginatedOrdersViewModel
        {
            Orders = orders,
            PaginationProperties = PaginationHelper.CalculateProperties(page,
                model.Count,
                perPage)
        });
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UnpaidOrders(int page = 1, int perPage = 8)
    {
        var model = await _orderService.GetAllUnpayedOrdersAsync();

        var orders = model.ToList()
            .Skip((page - 1) * perPage)
            .Take(perPage);

        ViewData["QueryParameters"] = new Dictionary<string, string>
        {
            { "Page", page.ToString() },
            { "PerPage", perPage.ToString() }
        };

        return View(new ReturnPaginatedOrdersViewModel
        {
            Orders = orders,
            PaginationProperties = PaginationHelper.CalculateProperties(page,
                model.Count,
                perPage)
        });
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> OrderForSending(int page = 1, int perPage = 8)
    {
        var model = await _orderService.GetAllUnprocessedOrders();

        var orders = model.ToList()
            .Skip((page - 1) * perPage)
            .Take(perPage);

        ViewData["QueryParameters"] = new Dictionary<string, string>
        {
            { "Page", page.ToString() },
            { "PerPage", perPage.ToString() }
        };

        return View(new ReturnPaginatedOrdersViewModel
        {
            Orders = orders,
            PaginationProperties = PaginationHelper.CalculateProperties(page,
                model.Count,
                perPage)
        });
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> SentOrders(int page = 1, int perPage = 8)
    {
        var model = await _orderService.GetAllSentOrders();

        var orders = model.ToList()
            .Skip((page - 1) * perPage)
            .Take(perPage);

        ViewData["QueryParameters"] = new Dictionary<string, string>
        {
            { "Page", page.ToString() },
            { "PerPage", perPage.ToString() }
        };

        return View(new ReturnPaginatedOrdersViewModel
        {
            Orders = orders,
            PaginationProperties = PaginationHelper.CalculateProperties(page,
                model.Count,
                perPage)
        });
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> FinishedOrders(int page = 1, int perPage = 8)
    {
        var model = await _orderService.GetAllFinishedOrders();

        var orders = model.ToList()
            .Skip((page - 1) * perPage)
            .Take(perPage);

        ViewData["QueryParameters"] = new Dictionary<string, string>
        {
            { "Page", page.ToString() },
            { "PerPage", perPage.ToString() }
        };

        return View(new ReturnPaginatedOrdersViewModel
        {
            Orders = orders,
            PaginationProperties = PaginationHelper.CalculateProperties(page,
                model.Count,
                perPage)
        });
    }
}