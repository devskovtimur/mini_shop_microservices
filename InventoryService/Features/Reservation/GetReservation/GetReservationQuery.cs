using InventoryService.Features.Reservation.Dtos;
using Shared.Mediator;

namespace InventoryService.Features.Reservation.GetReservation;

public record GetReservationQuery(Guid OrderId) : IQuery<ReservationDto>;