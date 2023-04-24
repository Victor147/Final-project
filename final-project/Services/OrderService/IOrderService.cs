using final_project.Data.Entities;

namespace final_project.Services.OrderService;

public interface IOrderService
{
    Task CreateOrderAsync(Order order);
    Task<Order> ReadOrderAsync(int id);
    Task UpdatePaymentStatus(int id);
    Task<List<Order>> GetAllOrdersAsync();
    Task<List<Order>> GetAllPaidOrdersAsync();
    Task<List<Order>> GetAllUnpayedOrdersAsync();
    Task<List<Order>> GetAllOrdersForUserAsync(int userId);
}