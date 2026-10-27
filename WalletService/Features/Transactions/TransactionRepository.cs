using System.Data;
using Dapper;
using WalletService.Features.Transactions.Abstractions;
using WalletService.Shared.Infrastructure.Abstractions;

namespace WalletService.Features.Transactions;

public class TransactionRepository(
    IDbConnectionProvider dbConnectionProvider,
    IDbTransactionProvider dbTransactionProvider) : ITransactionRepository
{
    private IDbConnection Connection => dbConnectionProvider.GetConnection();
    private IDbTransaction? Transaction => dbTransactionProvider.GetTransaction();


    public async Task<Transaction?> GetById(Guid id, CancellationToken ct = default)
    {
        const string sql = "SELECT * FROM transactions WHERE id = @Id;";

        return await Connection.QuerySingleOrDefaultAsync<Transaction>(sql,
            new { Id = id }, transaction: Transaction);
    }

    public async Task<IEnumerable<Transaction>> GetTransactions(
        Guid walletAndOwnerId,
        long pageSize,
        long page,
        CancellationToken ct = default)
    {
        const string sql = @"SELECT * FROM transactions 
                             WHERE wallet_and_owner_id = @WalletAndOwnerId
                             ORDER BY id 
                             LIMIT @PageSize
                             OFFSET @OffSet;";
        var offset = (pageSize - 1) * page;

        return await Connection.QueryAsync<Transaction>(sql,
            new
            {
                WalletAndOwnerId = walletAndOwnerId,
                PageSize = pageSize,
                OffSet = offset
            },
            transaction: Transaction);
    }

    public async Task<bool> Exists(Guid id, CancellationToken ct = default)
    {
        var entity = await GetById(id, ct: ct);
        return entity != null;
    }

    public async Task Add(Transaction entity, CancellationToken ct = default)
    {
        const string sql =
            @"INSERT INTO wallets (id, amount, wallet_and_owner_id, transaction_type, created_at_utc, deleted_at_utc, is_deleted)
              VALUES (@Id, @Amount, @WalletAndOwnerId, @TransactionType, @CreatedAtUtc, @DeletedAtUtc, @IsDeleted);";

        await Connection.ExecuteAsync(sql,
            new
            {
                Id = entity.Id,
                CreatedAtUtc = entity.CreatedAtUtc,
                DeletedAtUtc = entity.DeletedAtUtc,
                IsDeleted = entity.IsDeleted,

                WalletAndOwnerId = entity.WalletAndOwnerId,
                Amount = entity.Amount,
                TransactionType = entity.TransactionType,
            },
            transaction: Transaction);
    }
}