using WalletService.Features.Transactions.CreateTransaction;
using WalletService.Features.Transactions.GetTransactionsOfWallet;
using WalletService.Features.Wallets.CreateWallet;
using WalletService.Features.Wallets.GetWallet;

namespace WalletService.Shared.Extensions;

public static class WebApplicationExtensions
{
    public static void MapWalletEndpoints(this WebApplication app)
    {
        app.MapGetWallet();
        app.MapCreateWallet();
    }
    
    public static void MapTransactionEndpoints(this WebApplication app)
    {
        app.MapGetTransactionsOfWallet();
        app.MapCreateTransaction();
    }
}