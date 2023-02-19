using System.Net;
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
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateProduct([FromForm] ProductModel model)
    {
        await _productService.CreateProductAsync(model);
        return RedirectToAction("Index", "Product");
    }

    [HttpGet]
    public async Task<IActionResult> Read(int id)
    {
        Product product = await _productService.ReadProductAsync(id);
        ProductViewModel productVM = _mapper.Map<ProductViewModel>(product);

        return View(productVM);
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        Product product = await _productService.ReadProductAsync(id);
        UpdateProductModel productVM = _mapper.Map<UpdateProductModel>(product);

        return View(productVM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateProduct([FromForm] UpdateProductModel model)
    {
        ProductModel productModel = _mapper.Map<ProductModel>(model);
        int id = model.Id;

        await _productService.UpdateProductAsync(id, productModel);
        return RedirectToAction("Index", "Product");
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        Product product = await _productService.ReadProductAsync(id);
        ProductViewModel productVM = _mapper.Map<ProductViewModel>(product);

        return View(productVM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteProduct(ProductViewModel productViewModel)
    {
        await _productService.DeleteProductAsync(productViewModel.Id);
        
        return RedirectToAction("Index", "Product");
    }
}