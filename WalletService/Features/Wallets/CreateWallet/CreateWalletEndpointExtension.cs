using MediatR;
using Shared.Extensions;

namespace WalletService.Features.Wallets.CreateWallet;

public static class CreateWalletEndpointExtension
{
    public static void MapCreateWallet(this WebApplication app)
    {
        app.MapPost("api/wallets",
                async (CreateWalletRequest request, HttpContext context, ISender sender, CancellationToken ct) =>
                {
                    if (context.User.TryGetIdFromAccessToken(out var userId) == false)
                    {
                        return Results.Unauthorized();
                    }

                    var result = await sender.Send(
                        new CreateWalletCommand(userId, request.DefaultBalance), ct);

                    return result.ToHttpResponse();
                })
            .WithName("CreateWallet")
            .WithDescription("Create a Wallet which attached to the User")
            .Produces<Guid>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status409Conflict)
            .RequireAuthorization();
    }
}