using AutoMapper;
using final_project.Models;
using final_project.Services.ProductService;
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
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] ProductModel model)
    {
        if (ModelState.IsValid)
        {
            await _productService.CreateProductAsync(model);
        }
        return RedirectToAction("Index", "Home");
    }
}