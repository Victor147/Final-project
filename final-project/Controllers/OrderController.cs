using final_project.Data.Entities;
using final_project.Models;
using final_project.Services.OrderDetailService;
using final_project.Services.OrderService;
using final_project.Services.ProductService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace final_project.Controllers;

public class OrderController : Controller
{
    private readonly IProductService _productService;
    private readonly UserManager<User> _userManager;
    private readonly IOrderDetailService _orderDetailService;
    private readonly IOrderService _orderService;

    public OrderController(IProductService productService, UserManager<User> userManager, IOrderDetailService orderDetailService, IOrderService orderService)
    {
        _productService = productService;
        _userManager = userManager;
        _orderDetailService = orderDetailService;
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<IActionResult> Index(CartModel model)
    {
        for (int i = 0; i < model.Items.Count; i++)
        {
            var product = await _productService.ReadProductAsync(model.Items[i].Product.Id);

            model.Items[i].Product = product;
        }
        
        var user = await _userManager.FindByNameAsync(model.User.Username);
        
        var order = new Order
        {
            OrderDate = DateTime.Now,
            TotalCost = model.Items.Sum(it => it.SubTotal),
            UserId = user.Id,
            Address = model.DeliveryInformation.Address,
            Town = model.DeliveryInformation.Town,
        };

        await _orderService.CreateOrderAsync(order);

        for (int i = 0; i < model.Items.Count; i++)
        {
            var orderDetail = new OrderDetail
            {
                ProductId = model.Items[i].Product.Id,
                Quantity = model.Items[i].Quantity,
                OrderId = order.Id
            };

            await _orderDetailService.CreateOrderDetailAsync(orderDetail);
        }

        //implement logic
        return RedirectToAction("Index", "Product");
    }
}