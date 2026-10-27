using Shared.Abstractions;
using WalletService.Features.Transactions;
using WalletService.Features.Transactions.Abstractions;
using WalletService.Features.Wallets;
using WalletService.Features.Wallets.Abstractions;
using WalletService.Shared.Infrastructure;
using WalletService.Shared.Infrastructure.Abstractions;

namespace WalletService.Shared.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void SetupDapper(this WebApplicationBuilder builder)
    {
        builder.Services.AddNpgsqlDataSource(
            builder.Configuration.GetConnectionString("PostgresqlDatabase")!);

        builder.Services.AddScoped<DatabaseContext>();
        builder.Services.AddScoped<IDbConnectionProvider>(sp => sp.GetRequiredService<DatabaseContext>());
        builder.Services.AddScoped<IDbTransactionProvider>(sp => sp.GetRequiredService<DatabaseContext>());
        builder.Services.AddScoped<IUnitOfWorkProvider>(sp => sp.GetRequiredService<DatabaseContext>());

        builder.Services.AddScoped<IWalletRepository, WalletRepository>();
        builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
    }

    // public static void SetupEntityFramework(this WebApplicationBuilder builder)
    // {
    //     ...
    // }
}