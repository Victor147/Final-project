using final_project.Data.Entities;

namespace final_project.Models;

public class ProductModel
{
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public int Stock { get; set; }
    
    public decimal Price { get; set; }
    
    public IFormFile? Image { get; set; }
    
    public int ManufacturerId { get; set; }

    public int CategoryId { get; set; }

    public IEnumerable<Manufacturer>? Manufacturers { get; set; }

    public IEnumerable<Category>? Categories { get; set; }
}