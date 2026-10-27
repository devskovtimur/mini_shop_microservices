using Npgsql;
using Shared.Abstractions;

namespace WalletService.Shared.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    public NpgsqlConnection Connection { get; init; }
    public NpgsqlTransaction? Transaction { get; private set; }

    public UnitOfWork(NpgsqlConnection connection)
    {
        Connection = connection;
    }

    public async Task BeginTransaction(CancellationToken ct = default)
    {
        if (Transaction is not null)
        {
            throw new InvalidOperationException(
                "A transaction has already been opened.");
        }

        Transaction = await Connection.BeginTransactionAsync(ct);
    }

    public async Task CommitTransaction(CancellationToken cancellationToken = default)
    {
        if (Transaction == null)
        {
            throw new InvalidOperationException("No transaction has been opened.");
        }

        try
        {
            await Transaction.CommitAsync(cancellationToken);
        }
        finally
        {
            await SetTransactionNullAsync();
        }
    }

    public async Task RollbackTransaction(CancellationToken cancellationToken = default)
    {
        if (Transaction == null)
        {
            throw new InvalidOperationException("No transaction has been opened.");
        }

        try
        {
            await Transaction.RollbackAsync(cancellationToken);
        }
        finally
        {
            await SetTransactionNullAsync();
        }
    }

    private void SetTransactionNull()
    {
        if (Transaction is null)
            return;

        Transaction.Dispose();
        Transaction = null;
    }

    private async ValueTask SetTransactionNullAsync()
    {
        if (Transaction is not null)
        {
            await Transaction.DisposeAsync();
            Transaction = null;
        }
    }

    public void Dispose()
    {
        Transaction?.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        if (Transaction != null)
        {
            await Transaction.DisposeAsync();
        }
    }
}