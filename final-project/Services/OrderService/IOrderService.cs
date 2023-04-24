using final_project.Data.Entities;

namespace final_project.Services.OrderService;

public interface IOrderService
{
    Task CreateOrderAsync(Order order);
    Task<Order> ReadOrderAsync(int id);
    Task UpdatePaymentStatusAsync(int id);
    Task SendOrderAsync(int id);
    Task FinishOrderAsync(int id);
    Task<List<Order>> GetAllPaidOrdersAsync();
    Task<List<Order>> GetAllUnpayedOrdersAsync();
    Task<List<Order>> GetAllUnprocessedOrders();
    Task<List<Order>> GetAllSentOrders();
    Task<List<Order>> GetAllFinishedOrders();
    Task<List<Order>> GetAllOrdersForUserAsync(int userId);
}