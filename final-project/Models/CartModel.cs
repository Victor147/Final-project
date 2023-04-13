using final_project.ViewModels;

namespace final_project.Models;

public class CartModel
{
    public List<CartItemModel> Items { get; set; }

    public UserModel User { get; set; }

    public DeliveryInformationViewModel DeliveryInformation { get; set; }
}