namespace final_project.Data.Entities
{
    public class Product : Entity<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int Stock { get; set; }

        public decimal Price { get; set; }
    }
}
