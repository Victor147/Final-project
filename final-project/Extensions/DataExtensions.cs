using final_project.Data.Persistence;
using Microsoft.EntityFrameworkCore;

namespace final_project.Extensions
{
    public static class DataExtensions
    {
        public static void AddPersistence(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<EcommerceDbContext>(opt =>
            opt.UseSqlServer(connectionString));
        }
    }
}
