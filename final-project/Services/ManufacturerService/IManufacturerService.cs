using final_project.Data.Entities;
using final_project.Models;
using final_project.ViewModels;

namespace final_project.Services.ManufacturerService;

public interface IManufacturerService
{
    Task CreateManufacturerAsync(ManufacturerModel model);
    Task<Manufacturer> ReadManufacturerAsync(int id);
    Task<Manufacturer?> ReadManufacturerByNameAsync(string name);
    Task<IEnumerable<Manufacturer>> GetAllManufacturersAsync();
    Task UpdateManufacturerAsync(ManufacturerViewModel model);
    Task DeleteManufacturerAsync(int id);
}