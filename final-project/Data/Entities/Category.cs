namespace final_project.Data.Entities;

public class Category : Entity<int>
{
    public string Name { get; set; }

    public IEnumerable<Product> Products { get; set; }
}