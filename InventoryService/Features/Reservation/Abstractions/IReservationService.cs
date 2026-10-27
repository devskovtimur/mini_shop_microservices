using Shared;
using Shared.Contracts.Events.Inventory;

namespace InventoryService.Features.Reservation.Abstractions;

public interface IReservationService
{
    public Task<Result<Guid>> ReserveProducts(
        Guid orderId,
        IReadOnlyCollection<InventoryItemToReserve> itemsToReserve,
        CancellationToken token = default);
    
    public Task<Result> CancelReservation(Guid reservationId, CancellationToken token = default);
}