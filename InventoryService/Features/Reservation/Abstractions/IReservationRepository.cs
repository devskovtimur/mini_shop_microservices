using Shared.Abstractions;

namespace InventoryService.Features.Reservation.Abstractions;

public interface IReservationRepository : IGenericRepository<Reservation, Guid>
{
    public Task UpdateReservationState(Reservation reservation, CancellationToken ct = default);
}