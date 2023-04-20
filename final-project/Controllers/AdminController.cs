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
    public async Task<IActionResult> FinishedOrders()
    {
        var orders = await _orderService.GetAllFinishedOrdersAsync();

        return View(orders);
    }
    
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> OrdersForProcessing()
    {
        var orders = await _orderService.GetAllOrdersForProcessingAsync();

        return View(orders);
    }
    
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> OrderDetails(int orderId)
    {
        var orderDetails = await _orderDetailsService.GetAllOrdersDetailByOrderIdAsync(orderId);

        return View(orderDetails);
    }
}