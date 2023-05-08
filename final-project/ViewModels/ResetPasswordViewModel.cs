using System.ComponentModel.DataAnnotations;
using final_project.Validations;

namespace final_project.ViewModels;

public class ResetPasswordViewModel
{
    [Required(ErrorMessage = "Имейлът е задължителен!")]
    [EmailExists]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Паролата е задължителна!")]
    [RegularExpression("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{6,32}$", ErrorMessage = "Паролата не покрива изискванията!")]
    public string Password { get; set; }
    
    [Required(ErrorMessage = "Повтарянето на паролата е задължително!")]
    [RegularExpression("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{6,32}$", ErrorMessage = "Паролата не покрива изискванията!")]
    public string ConfirmPassword { get; set; }
    
    public string Token { get; set; }
}