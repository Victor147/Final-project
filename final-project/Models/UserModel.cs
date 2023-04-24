using System.ComponentModel.DataAnnotations;

namespace final_project.Models;

public class UserModel
{
    public string Username { get; set; }
    
    [Required(ErrorMessage = "Името е задължително!")]
    public string FirstName { get; set; }
    
    [Required(ErrorMessage = "Фамилията е задължителна!")]
    public string LastName { get; set; }
}