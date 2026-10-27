using Shared.Mediator;
using WalletService.Features.Wallets.Dtos;

namespace WalletService.Features.Wallets.GetWallet;

public record GetWalletQuery(Guid OwnerId) : IQuery<UserWalletDto>;