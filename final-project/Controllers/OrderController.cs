using AutoMapper;
using final_project.Data.Entities;
using final_project.Helpers;
using final_project.Models;
using final_project.Services.OrderDetailService;
using final_project.Services.OrderService;
using final_project.Services.ProductService;
using final_project.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace final_project.Controllers;

public class OrderController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IOrderDetailService _orderDetailService;
    private readonly IOrderService _orderService;
    private readonly ISession _session;
    private readonly IMapper _mapper;
    private readonly IProductService _productService;
    private readonly IOrderDetailService _orderDetailsService;

    public OrderController(UserManager<User> userManager, IOrderDetailService orderDetailService,
        IOrderService orderService, IHttpContextAccessor httpContextAccessor, IMapper mapper,
        IProductService productService, IOrderDetailService orderDetailsService)
    {
        _userManager = userManager;
        _orderDetailService = orderDetailService;
        _orderService = orderService;
        _mapper = mapper;
        _productService = productService;
        _orderDetailsService = orderDetailsService;
        _session = httpContextAccessor.HttpContext!.Session;
        StripeConfiguration.ApiKey =
            "sk_test_51MxlqfIe9yuEDfkXNITQ0wdiDXD5wiQRqeHqSrH8lI7VTEkCtRUv6pPOSu3OADZJcWHUCXaWkiC10qeRN5xKxwJe009u5U8keK";
    }


    public async Task<IActionResult> Index(CartModel model)
    {
        var user = await _userManager.FindByNameAsync(model.User.Username);
        var session = _session.Get<CartModel>($"cart_{user.Id}");
        model.Items = session!.Items;

        var order = new Order
        {
            OrderDate = DateTime.Now,
            TotalCost = model.Items.Sum(it => it.SubTotal),
            UserId = user.Id,
            Address = model.DeliveryInformation.Address,
            Town = model.DeliveryInformation.Town,
            Courier = model.DeliveryInformation.Courier,
            IsPaid = false,
            OrderStatus = OrderStatusEnum.Processing
        };

        await _orderService.CreateOrderAsync(order);

        for (int i = 0; i < model.Items.Count; i++)
        {
            if (model.Items[i].Quantity <= model.Items[i].Product.Stock)
            {
                var product = model.Items[i].Product;

                var orderDetail = new OrderDetail
                {
                    ProductId = product.Id,
                    Quantity = model.Items[i].Quantity,
                    OrderId = order.Id
                };

                await _orderDetailService.CreateOrderDetailAsync(orderDetail);

                product.Stock -= model.Items[i].Quantity;

                await _productService.UpdateProductQuantityAsync(product);

                session = new CartModel
                {
                    Items = new List<CartItemModel>(),
                    User = _mapper.Map<UserModel>(user),
                    DeliveryInformation = new DeliveryInformationViewModel()
                };
                _session.Set($"cart_{user.Id}", session);
                ViewBag.total = "0.00";
            }
            else
            {
                //return error page
            }
        }

        return RedirectToAction("Index", "Product");
    }

    [HttpPost]
    public async Task<IActionResult> RegisterCardOrder(User user)
    {
        user = await _userManager.FindByNameAsync(user.UserName);
        var model = _session.Get<CartModel>($"cart_{user.Id}");

        var order = new Order
        {
            OrderDate = DateTime.Now,
            TotalCost = model!.Items.Sum(it => it.SubTotal),
            UserId = user.Id,
            Address = model.DeliveryInformation.Address,
            Town = model.DeliveryInformation.Town,
            Courier = model.DeliveryInformation.Courier,
            IsPaid = true,
            OrderStatus = OrderStatusEnum.Processing
        };

        await _orderService.CreateOrderAsync(order);

        for (int i = 0; i < model.Items.Count; i++)
        {
            if (model.Items[i].Quantity <= model.Items[i].Product.Stock)
            {
                var product = model.Items[i].Product;

                var orderDetail = new OrderDetail
                {
                    ProductId = product.Id,
                    Quantity = model.Items[i].Quantity,
                    OrderId = order.Id
                };

                await _orderDetailService.CreateOrderDetailAsync(orderDetail);

                product.Stock -= model.Items[i].Quantity;

                await _productService.UpdateProductQuantityAsync(product);
            }
            else
            {
                //return error page
            }
        }
        
        model = new CartModel
        {
            Items = new List<CartItemModel>(),
            User = _mapper.Map<UserModel>(user),
            DeliveryInformation = new DeliveryInformationViewModel()
        };
        _session.Set($"cart_{user.Id}", model);
        ViewBag.total = "0.00";

        return RedirectToAction("Index", "Home");
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdatePaymentStatus(int orderId)
    {
        await _orderService.UpdatePaymentStatusAsync(orderId);

        return RedirectToAction("Panel", "Admin");
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> SendOrder(int orderId)
    {
        await _orderService.SendOrderAsync(orderId);

        return RedirectToAction("Panel", "Admin");
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> FinishOrder(int orderId)
    {
        await _orderService.FinishOrderAsync(orderId);

        return RedirectToAction("Panel", "Admin");
    }
    
    
    [Authorize(Roles = "Admin, User")]
    public async Task<IActionResult> OrderDetails(int orderId)
    {
        var orderDetails = await _orderDetailsService.GetAllOrdersDetailByOrderIdAsync(orderId);

        return View(orderDetails);
    }
}