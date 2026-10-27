namespace Shared.Contracts.Events.Inventory;

public record ReserveInventory(Guid OrderId, IReadOnlyCollection<InventoryItemToReserve> Items);
public record InventoryItemToReserve(Guid ProductId, long Quantity);