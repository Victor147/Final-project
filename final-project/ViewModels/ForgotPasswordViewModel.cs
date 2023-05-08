using System.ComponentModel.DataAnnotations;
using final_project.Validations;

namespace final_project.ViewModels;

public class ForgotPasswordViewModel
{
    [Required(ErrorMessage = "Потребителското име е задължително!")]
    [UsernameExists]
    public string Username { get; set; }
}