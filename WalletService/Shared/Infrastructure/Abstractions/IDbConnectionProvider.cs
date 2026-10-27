using System.Data;

namespace WalletService.Shared.Infrastructure.Abstractions;

public interface IDbConnectionProvider
{
    IDbConnection GetConnection();
}