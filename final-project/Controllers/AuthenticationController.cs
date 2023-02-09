using AutoMapper;
using final_project.Data.Entities;
using final_project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace final_project.Controllers;

public class AuthenticationController : Controller
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly SignInManager<User> _signInManager;

    public AuthenticationController(IMapper mapper, UserManager<User> userManager, RoleManager<Role> roleManager, SignInManager<User> signInManager)
    {
        _mapper = mapper;
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Register()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterModel viewModel)
    {
        if (ModelState.IsValid)
        {
            if (viewModel.Password != viewModel.RepeatPassword)
            {
                ModelState.AddModelError("RepeatPassword", "Passwords don't match!");
                return View("Register", viewModel);
            }

            var user = _mapper.Map<User>(viewModel);
            var result = await _userManager.CreateAsync(user, viewModel.Password);

            if (result.Succeeded)
            {
                var userRole = await _roleManager.FindByNameAsync("User");
                await _userManager.AddToRoleAsync(user, userRole.Name);
                await _signInManager.SignInAsync(user, true);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            
            ModelState.AddModelError(string.Empty, "Invalid Register Attempt!");
        }

        return View("Register", viewModel);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Login()
    {
        return View();
    }

    
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(viewModel.Username, viewModel.Password,
                viewModel.RememberMe, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("Message", "Invalid data!");
        }

        return View("Login", viewModel);
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}