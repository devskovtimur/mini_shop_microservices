using MassTransit;
using Shared.Contracts.Events.Order;
using Shared.Contracts.Events.Payment;
using WalletService.Features.Transactions.Abstractions;

namespace WalletService.Shared.Messaging.Consumers;

public class ProcessPaymentForOrderConsumer(
    IPaymentService paymentService,
    ILogger<ProcessPaymentForOrderConsumer> logger)
    : IConsumer<ProcessPaymentForOrder>
{
    public async Task Consume(ConsumeContext<ProcessPaymentForOrder> context)
    {
        try
        {
            var orderId = context.Message.OrderId;

            var paymentResult = await paymentService.TakeMoney(
                walletAndOwnerId: context.Message.OwnerId,
                amount: context.Message.Amount,
                ct: context.CancellationToken);

            if (paymentResult.IsSuccess)
            {
                await context.Publish(
                    new PaymentSucceeded(orderId, context.Message.OwnerId, paymentResult.Value));
            }
            else
            {
                await context.Publish(new PaymentFailed(orderId,
                    $"Transaction creation failed: {paymentResult.Error!.Message}"));
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

        // await context.RespondAsync(message); - to send a response message back to the original sender
    }
}