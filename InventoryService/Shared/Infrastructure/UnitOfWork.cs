using MongoDB.Driver;
using Shared.Abstractions;

namespace InventoryService.Shared.Infrastructure;

public class UnitOfWork(IMongoClient mongoClient) : IUnitOfWork
{
    private IClientSessionHandle? _session;

    public async Task BeginTransaction(CancellationToken ct = default)
    {
        if (_session is not null)
        {
            throw new InvalidOperationException(
                "A MongoDb Session has already been opened.");
        }

        _session = await mongoClient.StartSessionAsync(cancellationToken: ct);
        _session.StartTransaction();
    }

    public async Task CommitTransaction(CancellationToken cancellationToken = default)
    {
        if (_session == null)
        {
            throw new InvalidOperationException("No MongoDb Session has been opened.");
        }

        try
        {
            await _session.CommitTransactionAsync(cancellationToken);
        }
        finally
        {
            SetSessionNull();
        }
    }

    public async Task RollbackTransaction(CancellationToken cancellationToken = default)
    {
        if (_session == null)
        {
            throw new InvalidOperationException("No MongoDb Session has been opened.");
        }

        try
        {
            await _session.AbortTransactionAsync(cancellationToken);
        }
        finally
        {
            SetSessionNull();
        }
    }

    private void SetSessionNull()
    {
        if (_session is null)
            return;

        _session.Dispose();
        _session = null;
    }

    public void Dispose()
    {
        _session?.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        _session?.Dispose();
        return ValueTask.CompletedTask;
    }
}