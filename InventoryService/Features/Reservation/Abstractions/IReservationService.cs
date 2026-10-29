using Shared;

namespace InventoryService.Features.Reservation.Abstractions;

public interface IReservationService
{
    public Task<Result<Guid>> ReserveProducts(
        Guid orderId,
        IReadOnlyCollection<InventoryItem> itemsToReserve,
        CancellationToken token = default);
    
    public Task<Result> CancelReservation(Guid reservationId, CancellationToken token = default);
}