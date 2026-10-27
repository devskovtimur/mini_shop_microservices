using Shared.Mediator;

namespace WalletService.Features.Wallets.CreateWallet;

public record CreateWalletCommand(Guid OwnerId, decimal DefaultBalance) : ICommand<Guid>;