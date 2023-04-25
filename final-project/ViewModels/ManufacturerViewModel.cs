using System.ComponentModel.DataAnnotations;

namespace final_project.ViewModels;

public class ManufacturerViewModel
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Името е задължително!")]
    public string Name { get; set; }
}