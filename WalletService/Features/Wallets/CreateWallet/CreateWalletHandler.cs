using Shared;
using Shared.Mediator;
using WalletService.Features.Wallets.Abstractions;

namespace WalletService.Features.Wallets.CreateWallet;

public class CreateWalletHandler(IWalletRepository walletRepository) : ICommandHandler<CreateWalletCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateWalletCommand request, CancellationToken cancellationToken)
    {
        var wallet = await walletRepository.GetById(request.OwnerId, ct: cancellationToken);

        if (wallet != null)
        {
            return Result<Guid>.Failure(Error.Conflict($"Wallet with id {request.OwnerId} was already exists"));
        }

        var creatingResult = UserWallet.CreateWallet(request.OwnerId, request.DefaultBalance);
        wallet = creatingResult.Value!;

        if (creatingResult.IsFailure)
        {
            return Result<Guid>.Failure(creatingResult.Error!);
        }

        await walletRepository.Add(wallet, ct: cancellationToken);
        return Result<Guid>.Success(wallet.Id);
    }
}