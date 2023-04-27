using AutoMapper;
using final_project.Helpers;
using final_project.Models;
using final_project.Services.CategoryService;
using final_project.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace final_project.Controllers;

public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    public CategoryController(ICategoryService categoryService, IMapper mapper)
    {
        _categoryService = categoryService;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Index(int page = 1, int perPage = 6)
    {
        var model = await _categoryService.GetAllCategoriesAsync();
        var categories = model.ToList()
            .Skip((page - 1) * perPage)
            .Take(perPage)
            .Select(c => _mapper.Map<CategoryViewModel>(c));

        ViewData["QueryParameters"] = new Dictionary<string, string>
        {
            { "Page", page.ToString() },
            { "PerPage", perPage.ToString() }
        };
        
        return View(new ReturnPaginatedCategoriesViewModel
        {
            Categories = categories,
            PaginationProperties = PaginationHelper.CalculateProperties(page, 
                model.Count(), 
                perPage)
        });
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateCategory([FromForm] CategoryModel model)
    {
        if (ModelState.IsValid)
        {
            await _categoryService.CreateCategoryAsync(model);
            return RedirectToAction("Index", "Category");
        }

        return View("Create");
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Read(int id)
    {
        var category = await _categoryService.ReadCategoryAsync(id);
        var categoryVm = _mapper.Map<CategoryViewModel>(category);

        return View(categoryVm);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id)
    {
        var category = await _categoryService.ReadCategoryAsync(id);
        var categoryVm = _mapper.Map<CategoryViewModel>(category);

        return View(categoryVm);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateCategory([FromForm] CategoryViewModel model)
    {
        if (ModelState.IsValid)
        {
            await _categoryService.UpdateCategoryAsync(model);
            return RedirectToAction("Index", "Category");
        }

        return View("Update", model);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var category = await _categoryService.ReadCategoryAsync(id);
        var categoryVm = _mapper.Map<CategoryViewModel>(category);

        return View(categoryVm);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteCategory([FromForm] CategoryViewModel model)
    {
        await _categoryService.DeleteCategoryAsync(model.Id);
        return RedirectToAction("Index", "Category");
    }
}