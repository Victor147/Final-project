using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace final_project.Controllers;

public class AdminController : Controller
{
    // GET
    [Authorize(Roles = "Admin")]
    public IActionResult Panel()
    {
        return View();
    }
}