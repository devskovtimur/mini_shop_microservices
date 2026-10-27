using MassTransit;

namespace OrderService.Shared.Messaging.States;

public class OrderCreatingStateMachine : MassTransitStateMachine<CreatedOrderState>
{
    public OrderCreatingStateMachine()
    {
    }
}