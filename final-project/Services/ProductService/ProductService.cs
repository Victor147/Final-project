using final_project.Data.Entities;
using final_project.Data.Persistence;
using final_project.Helpers.Cloudinary;
using final_project.Models;

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
            Price = model.Price, Image = result.Uri.ToString()
        };
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
    }

    public Task<ProductModel> ReadProductAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        throw new NotImplementedException();
    }

    public Task UpdateProductAsync(Product product)
    {
        throw new NotImplementedException();
    }

    public Task DeleteProductAsync(int id)
    {
        throw new NotImplementedException();
    }
}