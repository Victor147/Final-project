using final_project.Data.Entities;
using final_project.Data.Persistence;
using Microsoft.EntityFrameworkCore;

namespace final_project.Services.OrderDetailService;

public class OrderDetailService : IOrderDetailService
{
    private readonly EcommerceDbContext _context;

    public OrderDetailService(EcommerceDbContext context)
    {
        _context = context;
    }

    public async Task CreateOrderDetailAsync(OrderDetail orderDetail)
    {
        _context.OrderDetails.Add(orderDetail);
        await _context.SaveChangesAsync();
    }

    public async Task<OrderDetail> ReadOrderDetailAsync(int id)
    {
        var orderDetail = await _context.OrderDetails.FindAsync(id);

        return orderDetail;
    }

    public async Task<IEnumerable<OrderDetail>> GetAllOrdersDetailAsync()
    {
        var orderDetails = await _context.OrderDetails.Include(od => od.Product).ToListAsync();

        return orderDetails;
    }

    public async Task<IEnumerable<OrderDetail>> GetAllOrdersDetailByOrderIdAsync(int orderId)
    {
        var orderDetails = await _context.OrderDetails.Include(od => od.Product).Where(od => od.OrderId == orderId).ToListAsync();

        return orderDetails;
    }
}