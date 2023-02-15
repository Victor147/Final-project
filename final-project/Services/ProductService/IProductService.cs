using final_project.Data.Entities;
using final_project.Models;

namespace final_project.Services.ProductService;

public interface IProductService
{
    Task CreateProductAsync(ProductModel model);
    Task<ProductModel> ReadProductAsync(int id);
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task UpdateProductAsync(Product product);
    Task DeleteProductAsync(int id);
}