namespace Shared.Contracts.Events.Inventory;

public record ReserveInventory(Guid OrderId, IReadOnlyCollection<InventoryItem> Items);