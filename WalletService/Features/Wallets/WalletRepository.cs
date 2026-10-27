using System.Data;
using Dapper;
using WalletService.Features.Wallets.Abstractions;
using WalletService.Shared.Infrastructure.Abstractions;

namespace WalletService.Features.Wallets;

public class WalletRepository(IDbConnectionProvider dbConnectionProvider, IDbTransactionProvider dbTransactionProvider)
    : IWalletRepository
{
    private IDbConnection Connection => dbConnectionProvider.GetConnection();
    private IDbTransaction? Transaction => dbTransactionProvider.GetTransaction();

    public async Task<UserWallet?> GetById(Guid walletAndUserId, CancellationToken ct = default)
    {
        const string sql = "SELECT * FROM wallets WHERE id = @Id;";

        return await Connection.QuerySingleOrDefaultAsync<UserWallet>(sql,
            new { Id = walletAndUserId },
            transaction: Transaction);
    }

    public async Task<UserWallet?> GetByIdWithForUpdate(Guid walletAndUserId, CancellationToken ct = default)
    {
        const string sql = @"
            SELECT * FROM wallets 
            WHERE Id = @Id 
            FOR UPDATE;";

        return await Connection.QuerySingleOrDefaultAsync<UserWallet>(sql,
            new { Id = walletAndUserId },
            transaction: Transaction);
    }

    public async Task Add(UserWallet wallet, CancellationToken ct = default)
    {
        const string sql =
            @"INSERT INTO wallets (id, balance, created_at_utc, deleted_at_utc, is_deleted)
              VALUES (@Id, @Balance, @CreatedAtUtc, @DeletedAtUtc, @IsDeleted);";

        await Connection.ExecuteAsync(sql,
            new
            {
                Id = wallet.Id,
                CreatedAtUtc = wallet.CreatedAtUtc,
                DeletedAtUtc = wallet.DeletedAtUtc,
                IsDeleted = wallet.IsDeleted,
                Balance = wallet.Balance
            },
            transaction: Transaction);
    }

    public async Task UpdateBalance(UserWallet wallet, CancellationToken ct = default)
    {
        const string sql = @"UPDATE wallets
                             SET balance = @Balance
                             WHERE Id = @Id;";
        await Connection.ExecuteAsync(sql,
            new { Id = wallet.Id, Balance = wallet.Balance },
            transaction: Transaction);
    }

    public async Task Remove(UserWallet wallet, CancellationToken ct = default)
    {
        const string sql = @"UPDATE wallets
                             SET deleted_at_utc = @DeletedAtUtc,
                             is_deleted = true
                             WHERE Id = @Id;";
        wallet.DeleteEntity();

        await Connection.ExecuteAsync(sql,
            new { Id = wallet.Id, DeletedAtUtc = wallet.DeletedAtUtc },
            transaction: Transaction);
    }
}