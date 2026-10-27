using MediatR;
using Shared.Extensions;

namespace WalletService.Features.Transactions.GetTransactionsOfWallet;

public static class GetTransactionsOfWalletEndpointExtension
{
    public static void MapGetTransactionsOfWallet(this WebApplication app)
    {
        app.MapGet("api/transactions/{pageSize:long}/{page:long}",
                async (GetTransactionOfWalletRequest request, HttpContext context, ISender sender, CancellationToken ct) =>
                {
                    if (context.User.TryGetIdFromAccessToken(out var userId) == false)
                    {
                        return Results.Unauthorized();
                    }

                    var result = await sender.Send(
                        new GetTransactionsOfWalletQuery(userId, request.PageSize, request.Page), ct);

                    return result.ToHttpResponse();
                })
            .WithName("GetWalletTransactions")
            .WithDescription("Get the wallet transactions")
            .Produces<Guid>()
            .RequireAuthorization();
    }
}