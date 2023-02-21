using final_project.Data.Entities;
using final_project.Data.Persistence;
using final_project.Helpers.Cloudinary;
using final_project.Models;
using Microsoft.EntityFrameworkCore;

namespace final_project.Services.ProductService;

public class ProductService : IProductService
{
    private readonly EcommerceDbContext _context;
    private readonly IImageUploader _uploader;

    public ProductService(IImageUploader uploader, EcommerceDbContext context)
    {
        _uploader = uploader;
        _context = context;
    }

    public async Task CreateProductAsync(ProductModel model)
    {
        var result = await _uploader.UploadImageAsync(model.Image.Name, model.Image);
        var product = new Product
        {
            Name = model.Name, Description = model.Description, Stock = model.Stock,
            Price = model.Price, Image = result.Uri.ToString(), ManufacturerId = model.ManufacturerId
        };
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
    }

    public async Task<Product> ReadProductAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        
        return product;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        var products = await _context.Products.Include(p => p.Manufacturer).ToListAsync();

        return products;
    }
    

    public async Task UpdateProductAsync(int id, ProductModel model)
    {
        var result = await _uploader.UploadImageAsync(model.Image.Name, model.Image);
        
        var fromDb = await ReadProductAsync(id);
        fromDb.Name = model.Name;
        fromDb.Description = model.Description;
        fromDb.Stock = model.Stock;
        fromDb.Price = model.Price;
        fromDb.Image = result.Uri.ToString();
        fromDb.ManufacturerId = model.ManufacturerId;

        _context.Products.Update(fromDb);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(int id)
    {
        _context.Products.Remove(await ReadProductAsync(id));
        await _context.SaveChangesAsync();
    }
}