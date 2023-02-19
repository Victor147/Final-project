using final_project.ViewModels;

namespace final_project.Models;

public class UpdateProductModel
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public int Stock { get; set; }
    
    public decimal Price { get; set; }
    
    public string Image { get; set; }
    
    public IFormFile File { get; set; }
}