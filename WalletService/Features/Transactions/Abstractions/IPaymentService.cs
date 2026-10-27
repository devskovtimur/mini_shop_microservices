using Shared;

namespace WalletService.Features.Transactions.Abstractions;

public interface IPaymentService
{
    public Task<Result<Guid>> CreditMoney(Guid walletAndOwnerId, decimal amount, CancellationToken ct = default);
    public Task<Result<Guid>> TakeMoney(Guid walletAndOwnerId, decimal amount, CancellationToken ct = default);

    public Task<Result<Guid>> CreditOrTakeMoney(Guid walletAndOwnerId, decimal amount, TransactionType transactionType,
        CancellationToken ct = default);
}