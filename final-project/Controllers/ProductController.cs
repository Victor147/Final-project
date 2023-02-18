using AutoMapper;
using final_project.Data.Entities;
using final_project.Models;
using final_project.Services.ProductService;
using final_project.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace final_project.Controllers;

public class ProductController : Controller
{
    private readonly IMapper _mapper;
    private readonly IProductService _productService;

    public ProductController(IMapper mapper, IProductService productService)
    {
        _mapper = mapper;
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var model = await _productService.GetAllProductsAsync();

        var products = new List<ProductViewModel>();

        foreach (var p in model)
        {
            products.Add(_mapper.Map<ProductViewModel>(p));
        }

        return View(products);
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromForm] ProductModel model)
    {
        await _productService.CreateProductAsync(model);
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Update(Product product)
    {
        var viewModel = _mapper.Map<ProductViewModel>(product);
        return View(viewModel);
    }
    
    // [HttpPost]
    // public async Task<IActionResult> UpdateProduct([FromForm] )


    
}