using Shared;

namespace OrderService.Features.Order;

public interface IOrderRepository
{
    public Task<IEnumerable<Order>> GetOrders();
    public Task<Result> CreateOrder(Order order);
}