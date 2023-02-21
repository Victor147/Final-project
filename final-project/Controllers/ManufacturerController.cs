using AutoMapper;
using final_project.Data.Entities;
using final_project.Models;
using final_project.Services.ManufacturerService;
using final_project.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> Index()
    {
        var manufacturers = await _manufacturerService.GetAllManufacturersAsync();

        var manufacturersVm = new List<ManufacturerViewModel>();

        foreach (var man in manufacturers)
        {
            manufacturersVm.Add(_mapper.Map<ManufacturerViewModel>(man));
        }

        return View(manufacturersVm);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateManufacturer([FromForm] ManufacturerModel model)
    {
        await _manufacturerService.CreateManufacturerAsync(model);
        return RedirectToAction("Index", "Manufacturer");
    }

    [HttpGet]
    public async Task<IActionResult> Read(int id)
    {
        Manufacturer manufacturer = await _manufacturerService.ReadManufacturerAsync(id);
        ManufacturerViewModel manufacturerVm = _mapper.Map<ManufacturerViewModel>(manufacturer);

        return View(manufacturerVm);
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        Manufacturer manufacturer = await _manufacturerService.ReadManufacturerAsync(id);
        ManufacturerViewModel manufacturerVm = _mapper.Map<ManufacturerViewModel>(manufacturer);

        return View(manufacturerVm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateManufacturer([FromForm] ManufacturerViewModel model)
    {
        await _manufacturerService.UpdateManufacturerAsync(model);
        return RedirectToAction("Index", "Manufacturer");
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        Manufacturer manufacturer = await _manufacturerService.ReadManufacturerAsync(id);
        ManufacturerViewModel manufacturerVm = _mapper.Map<ManufacturerViewModel>(manufacturer);

        return View(manufacturerVm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteManufacturer([FromForm] ManufacturerViewModel model)
    {
        await _manufacturerService.DeleteManufacturerAsync(model.Id);
        return RedirectToAction("Index", "Manufacturer");
    }
}