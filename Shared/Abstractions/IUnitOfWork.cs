namespace Shared.Abstractions;

public interface IUnitOfWork : IDisposable, IAsyncDisposable
{
    Task BeginTransaction(CancellationToken cancellationToken = default);
    Task CommitTransaction(CancellationToken cancellationToken = default);
    Task RollbackTransaction(CancellationToken cancellationToken = default);
}