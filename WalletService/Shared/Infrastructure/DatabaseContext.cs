using System.Data;
using Npgsql;
using Shared.Abstractions;
using WalletService.Shared.Infrastructure.Abstractions;

namespace WalletService.Shared.Infrastructure;

public class DatabaseContext(NpgsqlDataSource source)
    : IDbConnectionProvider, IDbTransactionProvider, IUnitOfWorkProvider, IDisposable, IAsyncDisposable
{
    private NpgsqlConnection Connection { get; init; } = source.OpenConnection();
    private bool IsUnitOfWorkOpen => _unitOfWork is not null;
    private UnitOfWork? _unitOfWork;

    public IDbConnection GetConnection()
    {
        return Connection;
    }

    public IDbTransaction? GetTransaction()
    {
        return _unitOfWork?.Transaction;
    }

    public IUnitOfWork GetUnitOfWork()
    {
        _unitOfWork ??= new UnitOfWork(Connection);
        return _unitOfWork;
    }

    public async ValueTask DisposeAsync()
    {
        if (IsUnitOfWorkOpen)
        {
            await _unitOfWork!.DisposeAsync();
            _unitOfWork = null;
        }

        await Connection.DisposeAsync();
    }

    public void Dispose()
    {
        if (IsUnitOfWorkOpen)
        {
            _unitOfWork!.Dispose();
            _unitOfWork = null;
        }

        Connection.Dispose();
    }
}