using System.Net.Security;
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
    public IActionResult Register(string? beforePath)
    {
        if (beforePath == null)
        {
            return View(new RegisterModel
            {
                BeforePath = string.Empty
            });
        }
        
        return View(new RegisterModel
        {
            BeforePath = beforePath
        });
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

                if (viewModel.BeforePath != null)
                {
                    var paths = viewModel.BeforePath.Split('/');
                    var action = paths[0];
                    var controller = paths[1];
                    return viewModel.BeforePath != null ? RedirectToAction(action, controller) : RedirectToAction("Index", "Home");
                }

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
    public IActionResult Login(string? beforePath)
    {
        if (beforePath == null)
        {
            return View(new LoginModel
            {
                BeforePath = string.Empty
            });
        }
        
        return View(new LoginModel
        {
            BeforePath = beforePath
        });
        
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
                if (viewModel.BeforePath != null)
                {
                    var paths = viewModel.BeforePath.Split('/');
                    var action = paths[0];
                    var controller = paths[1];
                    return viewModel.BeforePath != null ? RedirectToAction(action, controller) : RedirectToAction("Index", "Home");
                }

                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("Message", "Invalid data!");
        }

        return View("Login", viewModel);
    }

    [HttpGet]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> UpdateProfile(string username)
    {
        var user = await _userManager.FindByNameAsync(username);

        var profile = _mapper.Map<ProfileModel>(user);

        return View(profile);
    }

    [HttpPost]
    [Authorize(Roles = "User")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateProfile(ProfileModel model)
    {
        if (ModelState.IsValid)
        {
            var fromDb = await _userManager.FindByNameAsync(model.Username);
            fromDb.FirstName = model.FirstName;
            fromDb.LastName = model.LastName;
            fromDb.Address = model.Address;
            fromDb.Email = model.Email;

            await _userManager.UpdateAsync(fromDb);

            return RedirectToAction("Profile", "Profile");
        }

        return View("UpdateProfile", model);
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}