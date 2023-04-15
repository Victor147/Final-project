using final_project.Data.Entities;

namespace final_project.Models;

public class OrderAdminModel
{
    public string Username { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal TotalCost { get; set; }
    
    public string Address { get; set; }
        
    public string Town { get; set; }
        
    public List<OrderDetail> Details { get; set; }
}
