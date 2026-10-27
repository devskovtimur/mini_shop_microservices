using Shared;

namespace OrderService.Features.Order;

public class OrderItem : BaseEntity
{
    public Guid ProductId { get; init; }
    public long Quantity { get; init; }
    public decimal PriceForOne { get; init; }
    
    public decimal TotalPrice => Quantity * PriceForOne;
}