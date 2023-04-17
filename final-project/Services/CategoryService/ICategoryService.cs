using final_project.Data.Entities;
using final_project.Models;
using final_project.ViewModels;

namespace final_project.Services.CategoryService;

public interface ICategoryService
{
    Task CreateCategoryAsync(CategoryModel model);
    Task<Category> ReadCategoryAsync(int id);
    Task<Category?> ReadCategoryByNameAsync(string name);
    Task<IEnumerable<Category>> GetAllCategoriesAsync();
    Task UpdateCategoryAsync(CategoryViewModel model);
    Task DeleteCategoryAsync(int id);
}