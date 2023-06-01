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

    [HttpPost]
    public async Task<IActionResult> Pay(CartModel model)
    {
        var user = await _userManager.FindByNameAsync(model.User.Username);
        var session = _session.Get<CartModel>($"cart_{user.Id}");
        model.Items = session!.Items;

        _session.Set($"cart_{user.Id}", model);

        var lineItems = new List<SessionLineItemOptions>();

        foreach (var item in model.Items)
        {
            var slio = new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = Convert.ToInt64(item.Product.Price * 100),
                    Currency = "bgn",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = item.Product.Name,
                        Images = new List<string>
                        {
                            item.Product.Image
                        }
                    }
                },
                Quantity = item.Quantity
            };

            lineItems.Add(slio);
        }

        var options = new SessionCreateOptions
        {
            LineItems = lineItems,
            Mode = "payment",
            SuccessUrl = $"https://thecuecorner.azurewebsites.net/Payment/Success?username={model.User.Username}",
            CancelUrl = $"https://thecuecorner.azurewebsites.net/Payment/Failure?username={model.User.Username}",
        };

        var service = new SessionService();
        Session sessionConsent = service.Create(options);

        Response.Headers.Add("Location", sessionConsent.Url);
        return new StatusCodeResult(303);
    }

    [HttpGet]
    public async Task<IActionResult> Success(string username)
    {
        var user = await _userManager.FindByNameAsync(username);

        return View(user);
    }

    [HttpGet]
    public async Task<IActionResult> Failure(string username)
    {
        var user = await _userManager.FindByNameAsync(username);
        var session = _session.Get<CartModel>($"cart_{user.Id}");
        
        _session.Set($"cart_{user.Id}", session);

        TempData["PaymentMessage"] = "Плащането е неуспешно! Опитайте отново да завършите поръчката!";
        
        return RedirectToAction("Index", "Cart");
    }
}