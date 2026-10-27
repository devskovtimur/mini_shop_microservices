namespace Shared.Contracts.Events.Payment;

public class PaymentSucceeded(Guid OrderId, Guid OwnerId, Guid TransactionId);