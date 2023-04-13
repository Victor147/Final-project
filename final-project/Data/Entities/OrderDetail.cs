using System.ComponentModel.DataAnnotations.Schema;

namespace final_project.Data.Entities
{
    public class OrderDetail : Entity<int>
    {
        public Product? Product { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        
        public Order? Order { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }

        public int Quantity { get; set; }
    }
}
