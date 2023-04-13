using final_project.Data.Entities;

namespace final_project.Services.OrderDetailService;

public interface IOrderDetailService
{
    Task CreateOrderDetailAsync(OrderDetail orderDetail);
    Task<OrderDetail> ReadOrderDetailAsync(int id);
    Task<IEnumerable<OrderDetail>> GetAllOrdersDetailAsync();
}