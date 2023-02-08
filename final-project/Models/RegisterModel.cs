using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using final_project.Validations;

namespace final_project.Models;

public class RegisterModel
{
    [Required(ErrorMessage = "First Name is required!")]
    [DisplayName("First name: ")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last Name is required!")]
    [DisplayName("Last name: ")]
    public string LastName { get; set; }
    
    [Required(ErrorMessage = "Address is required!")]
    [DisplayName("Address: ")]
    public string Address { get; set; }
    
    [Required(ErrorMessage = "Email is required!")]
    [DisplayName("Email: ")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Username is required!")]
    [UsernameAvailable]
    [DisplayName("Username: ")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password is required!")]
    [DisplayName("Password: ")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Please repeat the password!")]
    [DisplayName("Repeat password: ")]
    public string RepeatPassword { get; set; }
}