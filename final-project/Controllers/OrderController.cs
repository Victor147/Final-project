using AutoMapper;
using final_project.Data.Entities;
using final_project.Helpers;
using final_project.Models;
using final_project.Services.OrderDetailService;
using final_project.Services.OrderService;
using final_project.Services.ProductService;
using final_project.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace final_project.Controllers;

public class OrderController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IOrderDetailService _orderDetailService;
    private readonly IOrderService _orderService;
    private readonly ISession _session;
    private readonly IMapper _mapper;
    private readonly IProductService _productService;

    public OrderController(UserManager<User> userManager, IOrderDetailService orderDetailService, IOrderService orderService, IHttpContextAccessor httpContextAccessor, IMapper mapper, IProductService productService)
    {
        _userManager = userManager;
        _orderDetailService = orderDetailService;
        _orderService = orderService;
        _mapper = mapper;
        _productService = productService;
        _session = httpContextAccessor.HttpContext!.Session;
    }

    [HttpPost]
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
            IsProcessed = model.DeliveryInformation.PaymentMethod == "card" ? true : false
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
}