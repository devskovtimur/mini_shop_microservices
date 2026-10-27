using Shared;
using Shared.Mediator;

namespace OrderService.Features.Order.CreateOrder;

public class CreateOrderHandler : ICommandHandler<CreateOrderCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var newOrderId = Guid.NewGuid();
        
        return Result<Guid>.Success(newOrderId);
    }
}