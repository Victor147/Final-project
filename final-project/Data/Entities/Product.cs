using System.ComponentModel.DataAnnotations.Schema;

namespace final_project.Data.Entities
{
    public class Product : Entity<int>
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public int Stock { get; set; }

        public decimal Price { get; set; }

        public string Image { get; set; }

        [NotMapped]
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}
