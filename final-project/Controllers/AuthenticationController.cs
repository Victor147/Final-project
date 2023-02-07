using Microsoft.AspNetCore.Mvc;

namespace final_project.Controllers;

public class AuthenticationController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}