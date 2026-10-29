using AutoMapper;
using InventoryService.Features.Reservation.Abstractions;
using InventoryService.Features.Reservation.Dtos;
using Shared;
using Shared.Mediator;

namespace InventoryService.Features.Reservation.GetReservation;

public class GetReservationHandler(
    IReservationRepository reservationRepository,
    IMapper mapper)
    : IQueryHandler<GetReservationQuery, ReservationDto>
{
    public async Task<Result<ReservationDto>> Handle(
        GetReservationQuery request,
        CancellationToken cancellationToken)
    {
        var reservation = await reservationRepository.GetByOrderId(request.OrderId, cancellationToken);

        if (reservation is null)
        {
            return Result<ReservationDto>.Failure(
                Error.NotFound($"Order {request.OrderId} with ID doesn't have a reservation, perhaps this Order doesn't exist at all"));
        }

        var dto = mapper.Map<ReservationDto>(reservation);
        return Result<ReservationDto>.Success(dto);
    }
}