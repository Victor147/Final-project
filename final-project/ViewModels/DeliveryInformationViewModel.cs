using System.ComponentModel.DataAnnotations;

namespace final_project.ViewModels;

public class DeliveryInformationViewModel
{
    [Required(ErrorMessage = "Телефонният номер е задължителен!")]
    [Phone]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Адресът е задължителен!")]
    public string Address { get; set; }

    [Required(ErrorMessage = "Градът е задължителен!")]
    public string Town { get; set; }
    
    public string Courier { get; set; }

    public string PaymentMethod { get; set; }
}