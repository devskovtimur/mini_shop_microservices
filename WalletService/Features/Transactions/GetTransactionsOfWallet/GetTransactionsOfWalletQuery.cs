using Shared.Mediator;
using WalletService.Features.Transactions.Dtos;

namespace WalletService.Features.Transactions.GetTransactionsOfWallet;

public record GetTransactionsOfWalletQuery(Guid WalletId, long PageSize, long Page) : IQuery<IEnumerable<TransactionDto>>;