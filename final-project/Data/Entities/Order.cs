using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using final_project.Data.Entities.Enums;

namespace final_project.Data.Entities
{
    public class Order : Entity<int>
    {
        public DateTime OrderDate { get; set; }

        public decimal TotalCost { get; set; }

        public User User { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public string Address { get; set; }
        
        public string Town { get; set; }
        
        public string Courier { get; set; }
        
        public bool IsPaid { get; set; }

        public string PhoneNumber { get; set; }
        
        public OrderStatusEnum OrderStatus { get; set; }

        public IEnumerable<OrderDetail> Details { get; set; }
        
    }
}
