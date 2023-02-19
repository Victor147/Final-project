using final_project.Data.Entities;
using final_project.Models;

namespace final_project.Services.ProductService;

public interface IProductService
{
    Task CreateProductAsync(ProductModel model);
    Task<Product> ReadProductAsync(int id);
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task UpdateProductAsync(int id, ProductModel model);
    Task DeleteProductAsync(int id);
}