namespace final_project.Data.Entities;

public class CartItem
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