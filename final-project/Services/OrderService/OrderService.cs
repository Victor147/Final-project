using final_project.Data.Entities;
using final_project.Data.Entities.Enums;
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
        var order = await _context.Orders.Include(or => or.User).FirstOrDefaultAsync(or => or.Id == id);

        return order!;
    }

    public async Task UpdatePaymentStatusAsync(int id)
    {
        var fromDb = await ReadOrderAsync(id);
        fromDb.IsPaid = true;
        fromDb.OrderStatus = OrderStatusEnum.Processing;

        _context.Orders.Update(fromDb);
        await _context.SaveChangesAsync();
    }

    public async Task SendOrderAsync(int id)
    {
        var fromDb = await ReadOrderAsync(id);
        fromDb.OrderStatus = OrderStatusEnum.Sent;

        _context.Orders.Update(fromDb);
        await _context.SaveChangesAsync();
    }

    public async Task FinishOrderAsync(int id)
    {
        var fromDb = await ReadOrderAsync(id);
        fromDb.OrderStatus = OrderStatusEnum.Received;

        _context.Orders.Update(fromDb);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Order>> GetAllOrdersAsync()
    {
        var orders = await _context.Orders.Include(or => or.User).Include(or => or.Details).ToListAsync();

        return orders;
    }

    public async Task<List<Order>> GetAllPaidOrdersAsync()
    {
        var orders = await _context.Orders.Include(or => or.User).Include(or => or.Details).Where(or => or.IsPaid).ToListAsync();

        return orders;
    }

    public async Task<List<Order>> GetAllUnpayedOrdersAsync()
    {
        var orders = await _context.Orders.Include(or => or.User).Include(or => or.Details).Where(or => !or.IsPaid).ToListAsync();

        return orders;
    }

    public async Task<List<Order>> GetAllUnprocessedOrders()
    {
        var orders = await _context.Orders.Include(or => or.User).Include(or => or.Details).Where(or => or.OrderStatus == OrderStatusEnum.Processing).Where(or => or.IsPaid).ToListAsync();

        return orders;
    }

    public async Task<List<Order>> GetAllSentOrders()
    {
        var orders = await _context.Orders.Include(or => or.User).Include(or => or.Details).Where(or => or.OrderStatus == OrderStatusEnum.Sent).ToListAsync();

        return orders;
    }

    public async Task<List<Order>> GetAllFinishedOrders()
    {
        var orders = await _context.Orders.Include(or => or.User).Include(or => or.Details).Where(or => or.OrderStatus == OrderStatusEnum.Received).ToListAsync();

        return orders;
    }

    public async Task<List<Order>> GetAllUnfinishedOrdersForUserAsync(int userId)
    {
        var orders = await _context.Orders.Include(or => or.User).Include(or => or.Details)
            .Where(or => or.User.Id == userId).Where(or => or.OrderStatus != OrderStatusEnum.Received).ToListAsync();

        return orders;
    }

    public async Task<List<Order>> GetAllFinishedOrdersForUserAsync(int userId)
    {
        var orders = await _context.Orders.Include(or => or.User).Include(or => or.Details)
            .Where(or => or.User.Id == userId).Where(or => or.OrderStatus == OrderStatusEnum.Received).ToListAsync();

        return orders;
    }
}