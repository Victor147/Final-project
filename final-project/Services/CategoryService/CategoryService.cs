using final_project.Data.Entities;
using final_project.Data.Persistence;
using final_project.Models;
using final_project.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace final_project.Services.CategoryService;

public class CategoryService : ICategoryService
{
    private readonly EcommerceDbContext _context;

    public CategoryService(EcommerceDbContext context)
    {
        _context = context;
    }

    public async Task CreateCategoryAsync(CategoryModel model)
    {
        var category = new Category
        {
            Name = model.Name
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
    }

    public async Task<Category> ReadCategoryAsync(int id)
    {
        var category = await _context.Categories.FindAsync(id);

        return category;
    }

    public async Task<Category?> ReadCategoryByNameAsync(string name)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == name);
        
        return category;
    }

    public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
    {
        var categories = await _context.Categories.ToListAsync();

        return categories;
    }

    public async Task UpdateCategoryAsync(CategoryViewModel model)
    {
        var fromDb = await ReadCategoryAsync(model.Id);
        fromDb.Name = model.Name;

        _context.Categories.Update(fromDb);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCategoryAsync(int id)
    {
        _context.Categories.Remove(await ReadCategoryAsync(id));
        await _context.SaveChangesAsync();
    }
}