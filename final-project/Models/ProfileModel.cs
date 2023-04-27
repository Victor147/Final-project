using System.ComponentModel;

namespace final_project.Models;

public class ProfileModel
{
    [DisplayName("Име: ")]
    public string FirstName { get; set; }
    
    [DisplayName("Фамилия: ")]
    public string LastName { get; set; }
    
    [DisplayName("Адрес: ")]
    public string Address { get; set; }
    
    [DisplayName("Имейл: ")]
    public string Email { get; set; }
}