using Shared.Mediator;

namespace WalletService.Features.Transactions.CreateTransaction;

public record CreateTransactionCommand(
    decimal Amount,
    TransactionType TransactionType,
    Guid WalletAndOwnerId
) : ICommand<Guid>;