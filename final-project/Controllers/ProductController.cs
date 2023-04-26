using AutoMapper;
using final_project.Data.Entities;
using final_project.Models;
using final_project.Services.ManufacturerService;
using final_project.Services.ProductService;
using final_project.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using final_project.Helpers;
using final_project.Services.CategoryService;

namespace final_project.Controllers;

public class ProductController : Controller
{
    private readonly IMapper _mapper;
    private readonly IProductService _productService;
    private readonly IManufacturerService _manufacturerService;
    private readonly ICategoryService _categoryService;

    public ProductController(IMapper mapper, IProductService productService, IManufacturerService manufacturerService,
        ICategoryService categoryService)
    {
        _mapper = mapper;
        _productService = productService;
        _manufacturerService = manufacturerService;
        _categoryService = categoryService;
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ListProducts(int page = 1, int perPage = 6)
    {
        var model = await _productService.GetAllProductsAsync();
        var products = model.ToList()
            .Skip((page - 1) * perPage)
            .Take(perPage)
            .Select(p => _mapper.Map<ProductViewModel>(p));

        ViewData["QueryParameters"] = new Dictionary<string, string>
        {
            { "Page", page.ToString() },
            { "PerPage", perPage.ToString() }
        };

        return View(new ReturnPaginatedProductsViewModel()
        {
            Products = products,
            PaginationProperties = PaginationHelper.CalculateProperties(page,
                model.Count(),
                perPage)
        });
    }

    public async Task<IActionResult> Index(string? category, string? manufacturerFilter, decimal? minPriceFilter,
        decimal? maxPriceFilter,
        string sortOrder, int page = 1, int perPage = 8)
    {
        var model = await _productService.GetAllProductsAsync();
        var manufacturers = await _manufacturerService.GetAllManufacturersAsync();
        
        var names = manufacturers.Select(m => m.Name).Distinct().OrderBy(m => m).ToList();

        ViewBag.Manufacturers = new SelectList(names);

        if (!string.IsNullOrEmpty(category))
        {
            model = model.Where(p => p.Category.Name == category);
        }

        if (!string.IsNullOrEmpty(manufacturerFilter))
        {
            model = model.Where(p => p.Manufacturer.Name == manufacturerFilter);
        }

        if (minPriceFilter.HasValue)
        {
            model = model.Where(p => p.Price >= minPriceFilter.Value);
        }

        if (maxPriceFilter.HasValue)
        {
            model = model.Where(p => p.Price <= maxPriceFilter.Value);
        }

        switch (sortOrder)
        {
            case "name_desc":
                model = model.OrderByDescending(p => p.Name).ToList().AsQueryable();
                break;
            case "price_asc":
                model = model.OrderBy(p => p.Price).ToList().AsQueryable();
                break;
            case "price_desc":
                model = model.OrderByDescending(p => p.Price).ToList().AsQueryable();
                break;
            default:
                model = model.OrderBy(p => p.Name).ToList().AsQueryable();
                break;
        }

        ViewBag.Category = category;
        ViewBag.SortOrder = sortOrder;
        ViewBag.MinPriceFilter = minPriceFilter;
        ViewBag.MaxPriceFilter = maxPriceFilter;
        ViewBag.ManufacturerFilter = manufacturerFilter;
        
        model = model.Where(p => p.Stock != 0);

        var products = model.ToList()
            .Skip((page - 1) * perPage)
            .Take(perPage)
            .Select(p => _mapper.Map<ProductViewModel>(p));

        ViewData["QueryParameters"] = new Dictionary<string, string>
        {
            {
                "Page", page.ToString()
            },
            {
                "PerPage", perPage.ToString()
            }
        };

        return View(new ReturnPaginatedProductsViewModel
        {
            Products = products,
            PaginationProperties = PaginationHelper.CalculateProperties(page,
                model.Count(),
                perPage)
        });
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create()
    {
        ProductModel model = new ProductModel
        {
            Manufacturers = await _manufacturerService.GetAllManufacturersAsync(),
            Categories = await _categoryService.GetAllCategoriesAsync()
        };
        return View(model);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateProduct([FromForm] ProductModel model)
    {
        if (ModelState.IsValid)
        {
            await _productService.CreateProductAsync(model);
            return RedirectToAction("Index", "Product");
        }

        model.Categories = await _categoryService.GetAllCategoriesAsync();
        model.Manufacturers = await _manufacturerService.GetAllManufacturersAsync();

        return View("Create", model);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Read(int id)
    {
        Product product = await _productService.ReadProductAsync(id);
        ProductViewModel productVm = _mapper.Map<ProductViewModel>(product);
        productVm.Manufacturer = await _manufacturerService.ReadManufacturerAsync(product.ManufacturerId);
        productVm.Category = await _categoryService.ReadCategoryAsync(product.CategoryId);

        return View(productVm);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id)
    {
        Product product = await _productService.ReadProductAsync(id);
        UpdateProductModel productVm = _mapper.Map<UpdateProductModel>(product);
        productVm.Manufacturers = await _manufacturerService.GetAllManufacturersAsync();
        productVm.Categories = await _categoryService.GetAllCategoriesAsync();

        return View(productVm);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateProduct([FromForm] UpdateProductModel model)
    {
        if (ModelState.IsValid)
        {
            ProductModel productModel = _mapper.Map<ProductModel>(model);
            int id = model.Id;

            if (productModel.Image == null)
            {
                await _productService.UpdateProductAsync(id, productModel, false);
            }
            else
            {
                await _productService.UpdateProductAsync(id, productModel, true);
            }

            return RedirectToAction("Index", "Product");
        }

        model.Categories = await _categoryService.GetAllCategoriesAsync();
        model.Manufacturers = await _manufacturerService.GetAllManufacturersAsync();

        return View("Update", model);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        Product product = await _productService.ReadProductAsync(id);
        ProductViewModel productVm = _mapper.Map<ProductViewModel>(product);
        productVm.Manufacturer = await _manufacturerService.ReadManufacturerAsync(product.ManufacturerId);
        productVm.Category = await _categoryService.ReadCategoryAsync(product.CategoryId);

        return View(productVm);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteProduct([FromForm] ProductViewModel productViewModel)
    {
        await _productService.DeleteProductAsync(productViewModel.Id);

        return RedirectToAction("Index", "Product");
    }
}