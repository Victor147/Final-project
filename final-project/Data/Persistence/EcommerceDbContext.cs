using final_project.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace final_project.Data.Persistence
{
    public class EcommerceDbContext : IdentityDbContext<User, Role, int>
    {
        public EcommerceDbContext()
        {

        }

        public EcommerceDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        
        public DbSet<User> Users { get; set; }
    }
}
