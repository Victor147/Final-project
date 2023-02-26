using final_project.Data.Entities;

namespace final_project.Models;

public class CartItemModel
{
    public Product Product { get; set; }
    
    public int Quantity { get; set; }

    private decimal subTotal;
    public decimal SubTotal
    {
        get { return subTotal; }
        set { subTotal = Product.Price * Quantity; }
    }
}