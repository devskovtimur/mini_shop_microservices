using Shared;

namespace InventoryService.Features.Reservation.Dtos;

public class ReservationDto
{
    public Guid Id { get; init; }
    public DateTime CreatedAtUtc { get; init; }

    public DateTime? DeletedAtUtc { get; init; }
    public bool IsDeleted { get; init; }
    
    public required string Status { get; init; }
    public Guid OrderId { get; init; }
    public IReadOnlyCollection<InventoryItem> Items { get; init; }
}