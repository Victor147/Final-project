using System.ComponentModel.DataAnnotations;

namespace final_project.Data.Entities;

public class Manufacturer : Entity<int>
{ 
    public string Name { get; set; }

    public IEnumerable<Product> Products { get; set; }
}