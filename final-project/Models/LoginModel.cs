using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using final_project.Validations;

namespace final_project.Models;

public class LoginModel
{
    [Required(ErrorMessage = "Потребителското име е задължително!")]
    [UsernameExists]
    [DisplayName("Потребителско име")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Паролата е задължителна!")]
    [DisplayName("Парола  ")]
    public string Password { get; set; }

    [DisplayName("Запомни ме")] 
    public bool RememberMe { get; set; } = false;

    public string? Message { get; set; } = string.Empty;

    public string? BeforePath { get; set; }
}