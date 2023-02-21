using AutoMapper;
using final_project.Data.Entities;
using final_project.Models;
using final_project.Services.ManufacturerService;
using final_project.Services.ProductService;
using final_project.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace final_project.Controllers;

public class ProductController : Controller
{
    private readonly IMapper _mapper;
    private readonly IProductService _productService;
    private readonly IManufacturerService _manufacturerService;

    public ProductController(IMapper mapper, IProductService productService, IManufacturerService manufacturerService)
    {
        _mapper = mapper;
        _productService = productService;
        _manufacturerService = manufacturerService;
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
    public async Task<IActionResult> Create()
    {
        ProductModel model = new ProductModel
        {
            Manufacturers = await _manufacturerService.GetAllManufacturersAsync()
        };
        return View(model);
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
        ProductViewModel productVm = _mapper.Map<ProductViewModel>(product);
        productVm.Manufacturer = await _manufacturerService.ReadManufacturerAsync(product.ManufacturerId);

        return View(productVm);
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        Product product = await _productService.ReadProductAsync(id);
        UpdateProductModel productVm = _mapper.Map<UpdateProductModel>(product);
        productVm.Manufacturers = await _manufacturerService.GetAllManufacturersAsync();

        return View(productVm);
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
        ProductViewModel productVm = _mapper.Map<ProductViewModel>(product);
        productVm.Manufacturer = await _manufacturerService.ReadManufacturerAsync(product.ManufacturerId);

        return View(productVm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteProduct([FromForm]ProductViewModel productViewModel)
    {
        await _productService.DeleteProductAsync(productViewModel.Id);
        
        return RedirectToAction("Index", "Product");
    }
}