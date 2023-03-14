using System.Collections;
using final_project.Data.Entities;
using final_project.Models;

namespace final_project.Services.ProductService;

public interface IProductService
{
    Task CreateProductAsync(ProductModel model);
    Task<Product> ReadProductAsync(int id);
    Task<Product?> ReadProductByNameAsync(string name);
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task UpdateProductAsync(int id, ProductModel model, bool changeImg);
    Task DeleteProductAsync(int id);
}