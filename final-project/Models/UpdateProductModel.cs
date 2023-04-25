using System.ComponentModel.DataAnnotations;
using final_project.Data.Entities;

namespace final_project.Models;

public class UpdateProductModel
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Името е задължително!")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Описанието е задължително!")]
    public string Description { get; set; }
    
    [Required(ErrorMessage = "Количеството е задължително!")]
    public int Stock { get; set; }
    
    [Required(ErrorMessage = "Цената е задължителна!")]
    public decimal Price { get; set; }
    
    public string Image { get; set; }
    
    [Required(ErrorMessage = "Снимката е задължителна!")]
    public IFormFile? File { get; set; }
    
    public int ManufacturerId { get; set; }

    public IEnumerable<Manufacturer> Manufacturers { get; set; }
    
    public int CategoryId { get; set; }

    public IEnumerable<Category> Categories { get; set; }
}