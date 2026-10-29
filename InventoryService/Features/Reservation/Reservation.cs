using InventoryService.Features.ReservedProduct;
using Shared;

namespace InventoryService.Features.Reservation;


public class Reservation : BaseEntity
{
    private Reservation(Guid orderId, IReadOnlyCollection<InventoryItem> items)
    {
        OrderId = orderId;
        Items = items;
        Status = ReservationStatus.Active;
    }

    public Guid OrderId { get; private set; }
    public IReadOnlyCollection<InventoryItem> Items { get; private set; }
    public ReservationStatus Status { get; private set; }

    public static Result<Reservation> CreateReservation(Guid orderId,
        IReadOnlyCollection<InventoryItem> items)
    {
        var reservedProduct = new Reservation(orderId, items);
        return ReservationValidator.Validator.Validate(reservedProduct);
    }

    public Result Cancel()
    {
        if (Status != ReservationStatus.Active)
        {
            return Result.Failure(
                Error.InvalidData("Only active reservations can be cancelled"));
        }

        Status = ReservationStatus.Cancelled;
        return Result.Success();
    }
}