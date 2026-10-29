using MediatR;
using Shared.Extensions;
using WalletService.Features.Wallets.Dtos;

namespace WalletService.Features.Wallets.GetWallet;

public static class GetWalletEndpointExtension
{
    public static void MapGetWallet(this WebApplication app)
    {
        app.MapGet("api/wallets",
                async (HttpContext context, ISender sender, CancellationToken ct) =>
                {
                    if (context.User.TryGetIdFromAccessToken(out var userId) == false)
                    {
                        return Results.Unauthorized();
                    }

                    var result = await sender.Send(new GetWalletQuery(userId), ct);
                    return result.ToHttpResponse();
                })
            .WithName("GetWalletOfTheUser")
            .WithDescription("Get the User's Wallet")
            .Produces<UserWalletDto>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .RequireAuthorization();
    }
}