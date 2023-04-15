using final_project.Data.Entities;
using final_project.Data.Persistence;
using Microsoft.EntityFrameworkCore;

namespace final_project.Services.OrderService;

public class OrderService : IOrderService
{
    private readonly EcommerceDbContext _context;

    public OrderService(EcommerceDbContext context)
    {
        _context = context;
    }

    public async Task CreateOrderAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
    }

    public async Task<Order> ReadOrderAsync(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        
        return order;
    }

    public async Task<List<Order>> GetAllOrdersAsync()
    {
        var orders = await _context.Orders.Include(or => or.User).Include(or => or.Details).ToListAsync();
        
        return orders;
    }
}