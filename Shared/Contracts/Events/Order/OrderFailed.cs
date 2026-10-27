namespace Shared.Contracts.Events.Order;

public record OrderFailed(Guid OrderId, string Reason);