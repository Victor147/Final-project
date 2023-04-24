using System.ComponentModel.DataAnnotations;
using final_project.ViewModels;

namespace final_project.Models;

public class CartModel
{
    public List<CartItemModel> Items { get; set; }

    [Required]
    public UserModel User { get; set; }

    [Required]
    public DeliveryInformationViewModel DeliveryInformation { get; set; }
}