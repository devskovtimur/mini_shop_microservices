using Shared;
using Shared.Abstractions;

namespace InventoryService.Features.Reservation.Abstractions;

public interface IReservationRepository : IGenericRepository<Reservation, Guid>
{
    Task<Reservation?> GetByOrderId(Guid orderId, CancellationToken ct = default);
    Task<Result> UpdateReservationState(Reservation entity, CancellationToken ct = default);
}