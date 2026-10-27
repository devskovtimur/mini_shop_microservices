using MediatR;
using Shared.Extensions;

namespace WalletService.Features.Transactions.CreateTransaction;

public static class CreateTransactionEndpointsExtensions
{
    public static void MapCreateTransaction(this WebApplication app)
    {
        app.MapPost("api/transactions/take-money",
                async (TakeMoneyRequest request, HttpContext context, ISender sender, CancellationToken ct) =>
                {
                    if (context.User.TryGetIdFromAccessToken(out var userId) == false)
                    {
                        return Results.Unauthorized();
                    }

                    var result = await sender.Send(
                        new CreateTransactionCommand(request.Amount, TransactionType.Debit, userId), ct);

                    return result.ToHttpResponse();
                })
            .WithName("CreateTransactionToTakeMoney")
            .WithDescription("Create a Transaction to take money from the user's wallet")
            .Produces<Guid>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status409Conflict)
            .RequireAuthorization();


        app.MapPost("api/transactions/credit-money",
                async (CreditMoneyRequest request, HttpContext context, ISender sender, CancellationToken ct) =>
                {
                    if (context.User.TryGetIdFromAccessToken(out var userId) == false)
                    {
                        return Results.Unauthorized();
                    }

                    var result = await sender.Send(
                        new CreateTransactionCommand(request.Amount, TransactionType.Credit, userId), ct);

                    return result.ToHttpResponse();
                })
            .WithName("CreateTransactionToCreditMoney")
            .WithDescription("Create a Transaction to credit money from the user's wallet")
            .Produces<Guid>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status409Conflict)
            .RequireAuthorization();
    }
}