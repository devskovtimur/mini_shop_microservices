namespace Shared.Contracts.Events.Payment;

public record ProcessPaymentForOrder(Guid OwnerId, Guid OrderId, decimal Amount);