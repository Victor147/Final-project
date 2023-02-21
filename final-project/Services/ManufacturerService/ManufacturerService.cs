using final_project.Data.Entities;
using final_project.Data.Persistence;
using final_project.Models;
using final_project.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace final_project.Services.ManufacturerService;

public class ManufacturerService : IManufacturerService
{
    private readonly EcommerceDbContext _context;

    public ManufacturerService(EcommerceDbContext context)
    {
        _context = context;
    }

    public async Task CreateManufacturerAsync(ManufacturerModel model)
    {
        var manufacturer = new Manufacturer
        {
            Name = model.Name
        };
        _context.Manufacturers.Add(manufacturer);
        await _context.SaveChangesAsync();
    }

    public async Task<Manufacturer> ReadManufacturerAsync(int id)
    {
        var manufacturer = await _context.Manufacturers.FindAsync(id);
        
        return manufacturer;
    }

    public async Task<IEnumerable<Manufacturer>> GetAllManufacturersAsync()
    {
        var manufacturers = await _context.Manufacturers.ToListAsync();
        
        return manufacturers;
    }

    public async Task UpdateManufacturerAsync(ManufacturerViewModel model)
    {
        var fromDb = await ReadManufacturerAsync(model.Id);
        fromDb.Name = model.Name;

        _context.Manufacturers.Update(fromDb);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteManufacturerAsync(int id)
    {
        _context.Manufacturers.Remove(await ReadManufacturerAsync(id));
        await _context.SaveChangesAsync();
    }
}