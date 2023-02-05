using Microsoft.AspNetCore.Identity;

namespace final_project.Data.Entities
{
    public class User : IdentityUser<int>
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Address { get; set; }

        public IEnumerable<Order>? Orders { get; set; }

        public IEnumerable<OrderDetail>? OrderDetails { get; set; }

    }
}
