using MassTransit;

namespace OrderService.Shared.Messaging.States;

public class CreatedOrderState : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
}