using AutoMapper;
using final_project.Data.Entities;
using final_project.Helpers;
using final_project.Models;
using final_project.Services.ManufacturerService;
using final_project.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace final_project.Controllers;

public class ManufacturerController : Controller
{
    private readonly IMapper _mapper;
    private readonly IManufacturerService _manufacturerService;

    public ManufacturerController(IMapper mapper, IManufacturerService manufacturerService)
    {
        _mapper = mapper;
        _manufacturerService = manufacturerService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Index(int page = 1, int perPage = 6)
    {
        var model = await _manufacturerService.GetAllManufacturersAsync();
        var manufacturers = model.ToList()
            .Skip((page - 1) * perPage)
            .Take(perPage)
            .Select(m => _mapper.Map<ManufacturerViewModel>(m));
        
        ViewData["QueryParameters"] = new Dictionary<string, string>
        {
            { "Page", page.ToString() },
            { "PerPage", perPage.ToString() }
        };

        return View(new ReturnPaginatedManufacturersViewModel
        {
            Manufacturers = manufacturers,
            PaginationProperties = PaginationHelper.CalculateProperties(page, 
                model.Count(), 
                perPage)
        });
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateManufacturer([FromForm] ManufacturerModel model)
    {
        if (ModelState.IsValid)
        {
            await _manufacturerService.CreateManufacturerAsync(model);
            return RedirectToAction("Index", "Manufacturer");
        }

        return View("Create");
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Read(int id)
    {
        Manufacturer manufacturer = await _manufacturerService.ReadManufacturerAsync(id);
        ManufacturerViewModel manufacturerVm = _mapper.Map<ManufacturerViewModel>(manufacturer);

        return View(manufacturerVm);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id)
    {
        Manufacturer manufacturer = await _manufacturerService.ReadManufacturerAsync(id);
        ManufacturerViewModel manufacturerVm = _mapper.Map<ManufacturerViewModel>(manufacturer);

        return View(manufacturerVm);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateManufacturer([FromForm] ManufacturerViewModel model)
    {
        if (ModelState.IsValid)
        {
            await _manufacturerService.UpdateManufacturerAsync(model);
            return RedirectToAction("Index", "Manufacturer");
        }

        return View("Update", model);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        Manufacturer manufacturer = await _manufacturerService.ReadManufacturerAsync(id);
        ManufacturerViewModel manufacturerVm = _mapper.Map<ManufacturerViewModel>(manufacturer);

        return View(manufacturerVm);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteManufacturer([FromForm] ManufacturerViewModel model)
    {
        await _manufacturerService.DeleteManufacturerAsync(model.Id);
        return RedirectToAction("Index", "Manufacturer");
    }
}