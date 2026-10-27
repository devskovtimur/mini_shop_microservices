using Shared;
using WalletService.Features.Transactions.Validators;

namespace WalletService.Features.Transactions;

public class Transaction : BaseEntity
{
    private Transaction(decimal amount, TransactionType transactionType, Guid walletAndOwnerId)
    {
        Amount = amount;
        TransactionType = transactionType;
        WalletAndOwnerId = walletAndOwnerId;
    }

    public decimal Amount { get; init; }
    public TransactionType TransactionType { get; init; }

    public Guid WalletAndOwnerId { get; init; }
    public bool IsOwner(Guid userId) => WalletAndOwnerId == userId;

    public static Result<Transaction> CreateTransaction(decimal amount, Guid walletAndOwnerId,
        TransactionType transactionType)
    {
        var transaction = new Transaction(amount, transactionType, walletAndOwnerId);
        return TransactionValidator.Validator.Validate(transaction);
    }
}