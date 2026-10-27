using Shared;

namespace OrderService.Features.Order;

public class Order : BaseEntity
{
    public OrderStatus Status { get; init; }
    public List<OrderItem> Items { get; init; } = [];
    public decimal OrderTotalPrice => Items.Sum(x => x.TotalPrice);

    //TODO
    // public Guid PaymentTransactionId { get => _paymentTransactionId; set => set only when if _paymentTransactionId is null; }
    // public Guid _paymentTransactionId
}