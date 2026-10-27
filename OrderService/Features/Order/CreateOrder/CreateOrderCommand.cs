using Shared.Mediator;

namespace OrderService.Features.Order.CreateOrder;

public record CreateOrderCommand(
    string Name,
    string Description
) : ICommand<Guid>;