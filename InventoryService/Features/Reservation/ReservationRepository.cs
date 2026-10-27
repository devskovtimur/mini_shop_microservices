using InventoryService.Features.Reservation.Abstractions;

namespace InventoryService.Features.Reservation;

public class ReservationRepository : IReservationRepository
{
    public Task<Reservation?> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Exists(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task Add(Reservation entity)
    {
        throw new NotImplementedException();
    }

    public Task Update(Reservation entity)
    {
        throw new NotImplementedException();
    }

    public Task Remove(Reservation entity)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateReservationState(Reservation reservation, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}