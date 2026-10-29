using InventoryService.Features.Reservation.Abstractions;
using InventoryService.Shared.Extensions;
using MongoDB.Driver;
using Shared;

namespace InventoryService.Features.Reservation;

public class ReservationRepository(
    IMongoDatabase database) : IReservationRepository
{
    private readonly IMongoCollection<Reservation> _collection =
        database.GetCollection<Reservation>("reservations");

    public async Task<Reservation?> GetById(Guid id, CancellationToken ct = default)
    {
        var filter = Builders<Reservation>.Filter.GetFilterById(id);
        return await _collection.Find(filter).FirstOrDefaultAsync(ct);
    }

    public async Task<bool> Exists(Guid id, CancellationToken ct = default)
    {
        var reservation = await GetById(id, ct);
        return reservation is not null;
    }

    public async Task Add(Reservation entity, CancellationToken ct = default)
    {
        await _collection.InsertOneAsync(entity, cancellationToken: ct);
    }

    public async Task<Result> SoftRemove(Reservation entity, CancellationToken ct = default)
    {
        entity.DeleteEntity();
        var filter = Builders<Reservation>.Filter.GetFilterById(entity.Id);

        var update = Builders<Reservation>.Update
            .Set(x => x.DeletedAtUtc, entity.DeletedAtUtc)
            .Set(x => x.IsDeleted, entity.IsDeleted);

        var result = await _collection.UpdateOneAsync(filter, update, cancellationToken: ct);
        return GetResultDependingOnUpdateResult(result, entity.Id);
    }

    public async Task<Reservation?> GetByOrderId(Guid orderId, CancellationToken ct = default)
    {
        var filterBuilder = Builders<Reservation>.Filter;
        var filter = filterBuilder.Eq(x => x.OrderId, orderId) & filterBuilder.NotDeletedOnly();

        return await _collection.Find(filter).FirstOrDefaultAsync(ct);
    }

    public async Task<Result> UpdateReservationState(Reservation entity, CancellationToken ct = default)
    {
        var filter = Builders<Reservation>.Filter.GetFilterById(entity.Id);
        var update = Builders<Reservation>.Update
            .Set(x => x.Status, entity.Status);

        var result = await _collection.UpdateOneAsync(filter, update, cancellationToken: ct);
        return GetResultDependingOnUpdateResult(result, entity.Id);
    }

    private Result GetResultDependingOnUpdateResult(UpdateResult updateResult, Guid id)
    {
        if (updateResult.MatchedCount > 0)
        {
            return updateResult.ModifiedCount > 0
                ? Result.Success()
                : Result.Failure(Error.Conflict(
                    $"Reservation with {id} ID was found, " +
                    $"but nothing has changed because it already has the same values."));
        }

        return Result.Failure(
            Error.NotFound($"Reservation with {id} ID not found"));
    }
}