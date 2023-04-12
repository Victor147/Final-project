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

    public CartController(IProductService productService, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
    {
        _productService = productService;
        _userManager = userManager;
        _session = httpContextAccessor.HttpContext!.Session;
    }

    [Authorize]
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.FindByNameAsync(HttpContext.User.Identity!.Name);
        var userId = user.Id;
        
        var cart = _session.Get<List<CartItemModel>>($"cart_{userId}");
        if (cart != null)
        {
            if (cart.Sum(c => c.SubTotal) == 0)
            {
                ViewBag.total = "0.00";
            }
            else
            {
                ViewBag.total = cart.Sum(c => c.SubTotal);
            }
        }
        else
        {
            cart = new List<CartItemModel>();
            ViewBag.total = "0.00";
        }

        return View(cart);
    }

    [Authorize]
    public async Task<IActionResult> AddToCart(int id)
    {
        var user = await _userManager.FindByNameAsync(HttpContext.User.Identity!.Name);
        var userId = user.Id;
        
        var product = await _productService.ReadProductAsync(id);
        var cart = _session.Get<List<CartItemModel>>($"cart_{userId}");

        if (cart == null)
        {
            cart = new List<CartItemModel>();
            cart.Add(new CartItemModel
            {
                Product = product,
                Quantity = 1
            });
        }
        else
        {
            int index = cart.FindIndex(w => w.Product.Id == id);

            if (index != -1)
            {
                cart[index].Quantity++;
            }
            else
            {
                cart.Add(new CartItemModel
                {
                    Product = product,
                    Quantity = 1
                });
            }
        }
        
        _session.Set<List<CartItemModel>>($"cart_{userId}", cart);
        return RedirectToAction("Index", "Cart");
    }

    public async Task<IActionResult> IncreaseQuantity(int id)
    {
        var user = await _userManager.FindByNameAsync(HttpContext.User.Identity!.Name);
        var userId = user.Id;
        
        var cart = _session.Get<List<CartItemModel>>($"cart_{userId}");

        int index = cart.FindIndex(ci => ci.Product.Id == id);
        var cartItem = cart[index];
        
        cartItem.Quantity++;
        
        _session.Set<List<CartItemModel>>($"cart_{userId}", cart);
        return Json(new { success = true, subTotal = cartItem.SubTotal , total = cart.Sum(item => item.SubTotal) });
    }

    public async Task<IActionResult> ReduceQuantity(int id)
    {
        var user = await _userManager.FindByNameAsync(HttpContext.User.Identity!.Name);
        var userId = user.Id;
        
        var cart = _session.Get<List<CartItemModel>>($"cart_{userId}");
        
        int index = cart.FindIndex(ci => ci.Product.Id == id);
        var cartItem = cart[index];
        
        if (cartItem.Quantity == 1)
        {
            cart.RemoveAt(index);
        }
        else
        {
            cartItem.Quantity--;
        }
        
        _session.Set<List<CartItemModel>>($"cart_{userId}", cart);
        return Json(new { success = true, subTotal = cartItem.SubTotal , total = cart.Sum(item => item.SubTotal) });
    }

    public async Task<IActionResult> RemoveFromCart(int id)
    {
        var user = await _userManager.FindByNameAsync(HttpContext.User.Identity!.Name);
        var userId = user.Id;
        
        var cart = _session.Get<List<CartItemModel>>($"cart_{userId}");
        
        int index = cart.FindIndex(ci => ci.Product.Id == id);
        cart.RemoveAt(index);
        
        _session.Set<List<CartItemModel>>($"cart_{userId}", cart);
        return Json(new { total = cart.Sum(item => item.SubTotal) });
    }
}