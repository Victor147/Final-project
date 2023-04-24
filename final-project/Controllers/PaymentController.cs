using final_project.Data.Entities;
using final_project.Helpers;
using final_project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;

namespace final_project.Controllers;

public class PaymentController : Controller
{
    private readonly ISession _session;
    private readonly UserManager<User> _userManager;

    public PaymentController(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
    {
        _userManager = userManager;
        _session = httpContextAccessor.HttpContext!.Session;
        StripeConfiguration.ApiKey =
            "sk_test_51MxlqfIe9yuEDfkXNITQ0wdiDXD5wiQRqeHqSrH8lI7VTEkCtRUv6pPOSu3OADZJcWHUCXaWkiC10qeRN5xKxwJe009u5U8keK";
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var name = HttpContext.User.Identity!.Name;
        var user = await _userManager.FindByNameAsync(HttpContext.User.Identity!.Name);
        var cart = _session.Get<CartModel>($"cart_{user.Id}");

        return View(cart);
    }

    [HttpPost]
    public async Task<IActionResult> Pay(CartModel model)
    {
        var user = await _userManager.FindByNameAsync(model.User.Username);
        var session = _session.Get<CartModel>($"cart_{user.Id}");
        model.Items = session!.Items;

        var options = new SessionCreateOptions()
        {
            LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)model.Items[0].Product.Price,
                        Currency = "bgn",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = model.Items[0].Product.Name
                        },

                    },
                    Quantity = model.Items[0].Quantity
                },
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = Convert.ToInt64(model.Items[1].Product.Price),
                        Currency = "bgn",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = model.Items[1].Product.Name
                        },

                    },
                    Quantity = model.Items[1].Quantity
                }
            },
            Mode = "payment",
            SuccessUrl = "https://localhost:7255/Product/Index",
            CancelUrl = "https://localhost:7255/Home"
        };

        var service = new SessionService();
        Session sessionConsent = service.Create(options);

        Response.Headers.Add("Location", sessionConsent.Url);
        return new StatusCodeResult(303);
    }
}