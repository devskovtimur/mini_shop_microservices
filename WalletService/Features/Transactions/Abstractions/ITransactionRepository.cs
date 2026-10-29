namespace WalletService.Features.Transactions.Abstractions;

public interface ITransactionRepository
{
    Task<Transaction?> GetById(Guid id, CancellationToken ct = default);

    Task<IEnumerable<Transaction>> GetTransactions(
        Guid walletAndOwnerId,
        int pageSize,
        int page,
        CancellationToken ct = default);

    Task<bool> Exists(Guid id, CancellationToken ct = default);
    Task Add(Transaction entity, CancellationToken ct = default);
}