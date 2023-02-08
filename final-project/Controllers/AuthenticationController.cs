using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace final_project.Controllers;

public class AuthenticationController : Controller
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Register()
    {
        return View();
    }
    
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Login()
    {
        return View();
    }
}