namespace Shared.Contracts.Events.Inventory;

public class InventoryUnavailable(Guid OrderId, string Reason);