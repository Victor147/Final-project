using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace final_project.Models;

public class ProfileModel
{
    public string Username { get; set; }
    
    [DisplayName("Име: ")]
    [Required(ErrorMessage = "Името е задължително!")]
    public string FirstName { get; set; }
    
    [DisplayName("Фамилия: ")]
    [Required(ErrorMessage = "Фамилията е задължителна!")]
    public string LastName { get; set; }
    
    [DisplayName("Адрес: ")]
    [Required(ErrorMessage = "Адресът е задължителен!")]
    public string Address { get; set; }
    
    [DisplayName("Имейл: ")]
    [Required(ErrorMessage = "Имейлът е задължителен!")]
    public string Email { get; set; }
}