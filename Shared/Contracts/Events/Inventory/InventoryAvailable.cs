namespace Shared.Contracts.Events.Inventory;

public record InventoryAvailable(Guid OrderId, Guid ReservationId);