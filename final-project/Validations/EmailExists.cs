using System.ComponentModel.DataAnnotations;
using final_project.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace final_project.Validations;

public class EmailExists : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if(value is null)
            return ValidationResult.Success;
        var email = value.ToString();
        if(email is null)
            return ValidationResult.Success;

        var userService = (UserManager<User>)validationContext.GetService(typeof(UserManager<User>))!;

        return userService.FindByEmailAsync(email).Result != null
            ? ValidationResult.Success
            : new ValidationResult("Не съществува потребител с такъв имейл!");
    }
}