using MongoDB.Driver;
using Shared.Abstractions;

namespace InventoryService.Shared.Infrastructure;

public class DatabaseContext(IMongoClient client)
    : IUnitOfWorkProvider, IDisposable, IAsyncDisposable
{
    private bool IsUnitOfWorkOpen => _unitOfWork is not null;
    private UnitOfWork? _unitOfWork;

    public IUnitOfWork GetUnitOfWork()
    {
        _unitOfWork ??= new UnitOfWork(client);
        return _unitOfWork;
    }

    public async ValueTask DisposeAsync()
    {
        if (IsUnitOfWorkOpen)
        {
            await _unitOfWork!.DisposeAsync();
            _unitOfWork = null;
        }
    }

    public void Dispose()
    {
        if (IsUnitOfWorkOpen)
        {
            _unitOfWork!.Dispose();
            _unitOfWork = null;
        }
    }
}