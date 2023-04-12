using AutoMapper;
using final_project.Data.Entities;
using final_project.Extensions;
using final_project.Models;
using final_project.Services.ProductService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace final_project.Controllers;

public class CartController : Controller
{
    private readonly IProductService _productService;
    private readonly ISession _session;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public CartController(IProductService productService, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager, IMapper mapper)
    {
        _productService = productService;
        _userManager = userManager;
        _mapper = mapper;
        _session = httpContextAccessor.HttpContext!.Session;
    }

    [Authorize]
    public async Task<IActionResult> Index()
    {
        var name = HttpContext.User.Identity!.Name;
        var user = await _userManager.FindByNameAsync(HttpContext.User.Identity!.Name);
        var userModel = _mapper.Map<UserModel>(user);
        var userId = user.Id;
        
        var cart = _session.Get<CartModel>($"cart_{userId}");
        if (cart != null)
        {
            if (cart.Items.Sum(c => c.SubTotal) == 0)
            {
                ViewBag.total = "0.00";
            }
            else
            {
                ViewBag.total = cart.Items.Sum(c => c.SubTotal);
            }
        }
        else
        {
            cart = new CartModel
            {
                Items = new List<CartItemModel>(),
                User = userModel
            };
            ViewBag.total = "0.00";
        }
        
        return View(cart);
    }

    [Authorize]
    public async Task<IActionResult> AddToCart(int id)
    {
        var user = await _userManager.FindByNameAsync(HttpContext.User.Identity!.Name);
        var userModel = _mapper.Map<UserModel>(user);
        var userId = user.Id;
        
        var product = await _productService.ReadProductAsync(id);
        var cart = _session.Get<CartModel>($"cart_{userId}");

        if (cart == null)
        {
            cart = new CartModel
            {
                Items = new List<CartItemModel>(),
                User = userModel
            };
            
            cart.Items.Add(new CartItemModel
            {
                Product = product,
                Quantity = 1
            });
        }
        else
        {
            int index = cart.Items.FindIndex(w => w.Product.Id == id);

            if (index != -1)
            {
                cart.Items[index].Quantity++;
            }
            else
            {
                cart.Items.Add(new CartItemModel
                {
                    Product = product,
                    Quantity = 1
                });
            }
        }
        
        _session.Set($"cart_{userId}", cart);
        return RedirectToAction("Index", "Cart");
    }

    public async Task<IActionResult> IncreaseQuantity(int id)
    {
        var user = await _userManager.FindByNameAsync(HttpContext.User.Identity!.Name);
        var userId = user.Id;
        
        var cart = _session.Get<CartModel>($"cart_{userId}");

        int index = cart.Items.FindIndex(ci => ci.Product.Id == id);
        var cartItem = cart.Items[index];
        
        cartItem.Quantity++;
        
        _session.Set($"cart_{userId}", cart);
        return Json(new { success = true, subTotal = cartItem.SubTotal , total = cart.Items.Sum(item => item.SubTotal) });
    }

    public async Task<IActionResult> ReduceQuantity(int id)
    {
        var user = await _userManager.FindByNameAsync(HttpContext.User.Identity!.Name);
        var userId = user.Id;
        
        var cart = _session.Get<CartModel>($"cart_{userId}");
        
        int index = cart.Items.FindIndex(ci => ci.Product.Id == id);
        var cartItem = cart.Items[index];
        
        if (cartItem.Quantity == 1)
        {
            cart.Items.RemoveAt(index);
        }
        else
        {
            cartItem.Quantity--;
        }
        
        _session.Set($"cart_{userId}", cart);
        return Json(new { success = true, subTotal = cartItem.SubTotal , total = cart.Items.Sum(item => item.SubTotal) });
    }

    public async Task<IActionResult> RemoveFromCart(int id)
    {
        var user = await _userManager.FindByNameAsync(HttpContext.User.Identity!.Name);
        var userId = user.Id;
        
        var cart = _session.Get<CartModel>($"cart_{userId}");
        
        int index = cart.Items.FindIndex(ci => ci.Product.Id == id);
        cart.Items.RemoveAt(index);
        
        _session.Set($"cart_{userId}", cart);
        return Json(new { total = cart.Items.Sum(item => item.SubTotal) });
    }
}