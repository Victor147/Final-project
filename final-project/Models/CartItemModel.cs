using final_project.Data.Entities;

namespace final_project.Models;

public class CartItemModel
{
    public Product Product { get; set; }
    
    public int Quantity { get; set; }

    public decimal SubTotal
    {
        get { return Quantity * Product.Price; }
    }
}