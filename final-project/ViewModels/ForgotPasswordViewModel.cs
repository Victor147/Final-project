using System.ComponentModel.DataAnnotations;

namespace final_project.ViewModels;

public class ForgotPasswordViewModel
{
    [Required(ErrorMessage = "Потребителското име е задължително!")]
    public string Username { get; set; }
}