using AutoMapper;
using final_project.Models;
using final_project.Services.CategoryService;
using final_project.ViewModels;
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
    public async Task<IActionResult> Index()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();

        var categoriesVm = new List<CategoryViewModel>();

        foreach (var category in categories)
        {
            categoriesVm.Add(_mapper.Map<CategoryViewModel>(category));
        }

        return View(categoriesVm);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateCategory([FromForm] CategoryModel model)
    {
        await _categoryService.CreateCategoryAsync(model);
        return RedirectToAction("Index", "Category");
    }

    [HttpGet]
    public async Task<IActionResult> Read(int id)
    {
        var category = await _categoryService.ReadCategoryAsync(id);
        var categoryVm = _mapper.Map<CategoryViewModel>(category);

        return View(categoryVm);
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var category = await _categoryService.ReadCategoryAsync(id);
        var categoryVm = _mapper.Map<CategoryViewModel>(category);

        return View(categoryVm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateCategory([FromForm] CategoryViewModel model)
    {
        await _categoryService.UpdateCategoryAsync(model);
        return RedirectToAction("Index", "Category");
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var category = await _categoryService.ReadCategoryAsync(id);
        var categoryVm = _mapper.Map<CategoryViewModel>(category);

        return View(categoryVm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteCategory([FromForm] CategoryViewModel model)
    {
        await _categoryService.DeleteCategoryAsync(model.Id);
        return RedirectToAction("Index", "Category");
    }
}