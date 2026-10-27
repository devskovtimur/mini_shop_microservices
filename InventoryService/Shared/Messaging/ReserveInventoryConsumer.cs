using InventoryService.Features.Reservation.Abstractions;
using MassTransit;
using Shared.Contracts.Events.Inventory;
using Shared.Contracts.Events.Order;

namespace InventoryService.Shared.Messaging;

public class ReserveInventoryConsumer(
    ILogger<ReserveInventoryConsumer> logger,
    IReservationService reservationService)
    : IConsumer<ReserveInventory>
{
    public async Task Consume(ConsumeContext<ReserveInventory> context)
    {
        try
        {
            var reservationResult = await reservationService.ReserveProducts(
                context.Message.OrderId,
                context.Message.Items,
                context.CancellationToken);

            if (reservationResult.IsSuccess)
            {
                await context.Publish(
                    new InventoryAvailable(context.Message.OrderId,
                        reservationResult.Value));
            }
            else
            {
                await context.Publish(
                    new InventoryUnavailable(context.Message.OrderId,
                        $"Reservation failed: {reservationResult.Error!.Message}"));
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex,
                "Failed to process payment for order {OrderId}",
                context.Message.OrderId);

            await context.Publish<OrderFailed>(new
            {
                OrderId = context.Message.OrderId,
                Reason = "Payment processing error"
            });
        }
    }
}