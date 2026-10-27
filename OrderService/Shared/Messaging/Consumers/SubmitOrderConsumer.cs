using MassTransit;
using Shared.Contracts.Events.Order;

namespace OrderService.Shared.Messaging.Consumers;

public class SubmitOrderConsumer : IConsumer<OrderSubmitted>
{
    public async Task Consume(ConsumeContext<OrderSubmitted> context)
    {
        throw new NotImplementedException();
    }
}