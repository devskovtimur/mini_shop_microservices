using System.Collections.Concurrent;
using System.Collections.Immutable;
using InventoryService.Features.Product.Abstractions;
using InventoryService.Features.Reservation.Abstractions;
using Shared;
using Shared.Abstractions;
using Shared.Contracts.Events.Inventory;

namespace InventoryService.Features.Reservation;

public class ReservationService(
    ILogger<ReservationService> logger,
    IReservationRepository reservationRepository,
    IProductRepository productRepository,
    IUnitOfWorkProvider unitOfWorkProvider)
    : IReservationService
{
    public async Task<Result<Guid>> ReserveProducts(
        Guid orderId,
        IReadOnlyCollection<InventoryItemToReserve> itemsToReserve,
        CancellationToken token = default)
    {
        // 1) Create Reservation
        var creatingResult = Reservation.CreateReservation(orderId, itemsToReserve);

        if (creatingResult.IsFailure)
        {
            return Result<Guid>.Failure(creatingResult.Error!);
        }

        var reservation = creatingResult.Value!;

        // 2) Create Transaction
        var unitOfWork = unitOfWorkProvider.GetUnitOfWork();
        await unitOfWork.BeginTransaction(token);

        try
        {
            // 3) Get all needed products 
            var getProductTasks = itemsToReserve
                .ToDictionary(i => i.ProductId,
                    i => productRepository.GetById(i.ProductId, token));

            await Task.WhenAll(getProductTasks.Values);

            var products = getProductTasks
                .ToDictionary(x => x.Key,
                    x => x.Value.GetAwaiter().GetResult());

            // 4) Check if the needed products found
            foreach (var (id, product) in products)
            {
                if (product is not null)
                    continue;

                await unitOfWork.RollbackTransaction(token);

                return Result<Guid>.Failure(
                    Error.NotFound($"Product with {id} ID not found"));
            }

            // 5) Reserve items, Add Reservation entity and Update product with changed Quantity 
            foreach (var itemToReserve in itemsToReserve)
            {
                var product = products[itemToReserve.ProductId]!;
                var reservingResult = product.Reserve(itemToReserve.Quantity);

                if (reservingResult.IsFailure)
                {
                    await unitOfWork.RollbackTransaction(token);
                    return Result<Guid>.Failure(reservingResult.Error!);
                }

                await reservationRepository.Add(reservation, token);
                await productRepository.Update(product, token);
            }

            // 6) Commit
            await unitOfWork.CommitTransaction(token);
            return Result<Guid>.Success(reservation.Id);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to reserve products of the Order {OrderId}", orderId);
            await unitOfWork.RollbackTransaction(token);
            throw;
        }
    }

    public async Task<Result> CancelReservation(Guid reservationId,
        CancellationToken token = default)
    {
        // var reservation = await reservationRepository.GetById(reservationId, token);
        //
        // if (reservation == null)
        // {
        //     return Result.Failure(
        //         Error.NotFound($"Reservation with {reservationId} ID not found"));
        // }
        //
        // reservation.Cancel();
        // await reservationRepository.UpdateReservationState(reservation, token);

        // GET product and call AddProducts and refund the amount

        return Result.Success();
    }
}