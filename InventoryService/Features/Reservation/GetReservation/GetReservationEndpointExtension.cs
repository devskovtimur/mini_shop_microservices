using System.ComponentModel.DataAnnotations;
using InventoryService.Features.Product.Dtos;
using InventoryService.Features.Product.GetProducts;
using InventoryService.Features.Reservation.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions;

namespace InventoryService.Features.Reservation.GetReservation;

public static class GetReservationEndpointExtension
{
    public static void MapGetReservation(this WebApplication app)
    {
        app.MapGet("api/reservations/{orderId:guid}",
                async ([Required] Guid orderId,
                    ISender sender,
                    CancellationToken ct) =>
                {
                    var result = await sender.Send(
                        new GetReservationQuery(orderId), ct);

                    return result.ToHttpResponse();
                })
            .WithName("GetReservation")
            .WithDescription("Get reservation")
            .Produces<ReservationDto>();
    }
}