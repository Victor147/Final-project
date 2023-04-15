using final_project.Data.Entities;

namespace final_project.Services.OrderService;

public interface IOrderService
{
    Task CreateOrderAsync(Order order);
    Task<Order> ReadOrderAsync(int id);
    Task<List<Order>> GetAllOrdersAsync();
}