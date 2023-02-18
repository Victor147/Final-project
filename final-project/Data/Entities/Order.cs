namespace final_project.Data.Entities
{
    public class Order : Entity<int>
    {
        public DateTime OrderDate { get; set; }

        public decimal TotalCost { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }

        public IEnumerable<OrderDetail> Details { get; set; }
    }
}
