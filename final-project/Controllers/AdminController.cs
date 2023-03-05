using Microsoft.AspNetCore.Mvc;

namespace final_project.Controllers;

public class AdminController : Controller
{
    // GET
    public IActionResult Panel()
    {
        return View();
    }
}