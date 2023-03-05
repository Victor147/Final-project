using final_project.Extensions;
using final_project.Models;
using final_project.Services.ProductService;
using Microsoft.AspNetCore.Mvc;

namespace final_project.Controllers;

public class CartController : Controller
{
    private readonly IProductService _productService;
    private readonly ISession _session;

    public CartController(IProductService productService, IHttpContextAccessor httpContextAccessor)
    {
        _productService = productService;
        _session = httpContextAccessor.HttpContext!.Session;
    }

    public IActionResult Index()
    {
        var cart = _session.Get<List<CartItemModel>>("cart");
        if (cart != null)
        {
            ViewBag.total = cart.Sum(c => c.SubTotal);
        }
        else
        {
            cart = new List<CartItemModel>();
            ViewBag.total = 0;
        }

        return View(cart);
    }

    public async Task<IActionResult> AddToCart(int id)
    {
        var product = await _productService.ReadProductAsync(id);
        var cart = _session.Get<List<CartItemModel>>("cart");

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
        
        _session.Set<List<CartItemModel>>("cart", cart);
        return RedirectToAction("Index", "Cart");
    }

    public IActionResult IncreaseQuantity(int id)
    {
        //var product = await _productService.ReadProductAsync(id);
        var cart = _session.Get<List<CartItemModel>>("cart");

        int index = cart.FindIndex(ci => ci.Product.Id == id);
        cart[index].Quantity++;
        
        _session.Set<List<CartItemModel>>("cart", cart);
        return RedirectToAction("Index", "Cart");
    }

    public IActionResult ReduceQuantity(int id)
    {
        //var product = await _productService.ReadProductAsync(id);
        var cart = _session.Get<List<CartItemModel>>("cart");
        
        int index = cart.FindIndex(ci => ci.Product.Id == id);
        if (cart[index].Quantity == 1)
        {
            cart.RemoveAt(index);
        }
        else
        {
            cart[index].Quantity--;
        }
        
        _session.Set<List<CartItemModel>>("cart", cart);
        return RedirectToAction("Index", "Cart");
    }

    public IActionResult RemoveFromCart(int id)
    {
        var cart = _session.Get<List<CartItemModel>>("cart");
        
        int index = cart.FindIndex(ci => ci.Product.Id == id);
        cart.RemoveAt(index);
        
        _session.Set<List<CartItemModel>>("cart", cart);
        return RedirectToAction("Index", "Cart");
    }
}