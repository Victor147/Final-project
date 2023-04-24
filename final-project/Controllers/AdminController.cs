using AutoMapper;
using final_project.Services.OrderDetailService;
using final_project.Services.OrderService;
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
    public async Task<IActionResult> PaidOrders()
    {
        var orders = await _orderService.GetAllPaidOrdersAsync();

        return View(orders);
    }
    
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UnpayedOrders()
    {
        var orders = await _orderService.GetAllUnpayedOrdersAsync();

        return View(orders);
    }
    
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> OrderForSending()
    {
        var orders = await _orderService.GetAllUnprocessedOrders();

        return View(orders);
    }
    
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> SentOrders()
    {
        var orders = await _orderService.GetAllSentOrders();

        return View(orders);
    }
    
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> FinishedOrders()
    {
        var orders = await _orderService.GetAllFinishedOrders();

        return View(orders);
    }
    
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> OrderDetails(int orderId)
    {
        var orderDetails = await _orderDetailsService.GetAllOrdersDetailByOrderIdAsync(orderId);

        return View(orderDetails);
    }
}