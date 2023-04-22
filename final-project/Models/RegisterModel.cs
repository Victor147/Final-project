using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using final_project.Validations;

namespace final_project.Models;

public class RegisterModel
{
    [Required(ErrorMessage = "Собственото име е задължително!")]
    [DisplayName("Име")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Фамилията е задължителна!")]
    [DisplayName("Фамилия")]
    public string LastName { get; set; }
    
    [Required(ErrorMessage = "Адресът е задължителен!")]
    [DisplayName("Адрес")]
    public string Address { get; set; }
    
    [Required(ErrorMessage = "Имейлът е задължителен!")]
    [DisplayName("Имейл")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Потебителското име е задължително!")]
    [UsernameAvailable]
    [DisplayName("Потребителско име")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Паролата е задължителна!")]
    [DisplayName("Парола")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Повторянето на паролата е задължително!")]
    [DisplayName("Повтори паролата")]
    public string RepeatPassword { get; set; }

    public string? Message { get; set; } = string.Empty;

    public string? BeforePath { get; set; }
}