namespace WalletService.Features.Wallets.Abstractions;

public interface IWalletRepository
{
    Task<UserWallet?> GetById(Guid walletAndUserId, CancellationToken ct = default);

    Task<UserWallet?> GetByIdWithForUpdate(Guid walletAndUserId,
        CancellationToken ct = default);

    Task Add(UserWallet wallet, CancellationToken ct = default);

    Task UpdateBalance(UserWallet wallet, CancellationToken ct = default);
    Task Remove(UserWallet wallet, CancellationToken ct = default);
}