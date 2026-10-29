using Shared.Mediator;
using WalletService.Features.Transactions.Dtos;

namespace WalletService.Features.Transactions.GetTransactionsOfWallet;

public record GetTransactionsOfWalletQuery(Guid WalletId, int PageSize, int Page) : IQuery<IEnumerable<TransactionDto>>;