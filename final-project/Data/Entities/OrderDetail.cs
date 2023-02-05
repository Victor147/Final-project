namespace final_project.Data.Entities
{
    public class OrderDetail : Entity<int>
    {
        public Product Product { get; set; }

        public int ProductId { get; set; }

        public Order Order { get; set; }

        public int OrderId { get; set; }

        public int Quantity { get; set; }
    }
}
