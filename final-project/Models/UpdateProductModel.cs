using final_project.Data.Entities;

namespace final_project.Models;

public class UpdateProductModel
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public int Stock { get; set; }
    
    public decimal Price { get; set; }
    
    public string Image { get; set; }
    
    public IFormFile? File { get; set; }
    
    public int ManufacturerId { get; set; }

    public IEnumerable<Manufacturer> Manufacturers { get; set; }
    
    public int CategoryId { get; set; }

    public IEnumerable<Category> Categories { get; set; }
}