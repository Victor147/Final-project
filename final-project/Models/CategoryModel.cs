using System.ComponentModel.DataAnnotations;

namespace final_project.Models;

public class CategoryModel
{
    [Required(ErrorMessage = "Името е задължително!")]
    public string Name { get; set; }
}