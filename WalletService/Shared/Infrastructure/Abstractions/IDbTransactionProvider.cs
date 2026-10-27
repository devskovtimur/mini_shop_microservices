using System.Data;

namespace WalletService.Shared.Infrastructure.Abstractions;

public interface IDbTransactionProvider
{
    public IDbTransaction? GetTransaction();
}