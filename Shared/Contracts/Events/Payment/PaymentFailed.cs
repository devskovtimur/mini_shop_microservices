namespace Shared.Contracts.Events.Payment;

public record PaymentFailed(Guid OrderId, string Reason);