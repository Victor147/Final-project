using final_project.Data.Entities;

namespace final_project.Models;

public class CartModel
{
    public List<CartItemModel> Items { get; set; }

    public UserModel User { get; set; }
}