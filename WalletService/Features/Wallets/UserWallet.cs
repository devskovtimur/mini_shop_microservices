using Shared;
using WalletService.Features.Transactions;
using WalletService.Features.Wallets.Validators;

namespace WalletService.Features.Wallets;

public class UserWallet : BaseEntity
{
    private UserWallet(Guid ownerId, decimal balance)
    {
        Id = ownerId;
        Balance = balance;
    }

    public decimal Balance { get; private set; }
    public bool IsOwner(Guid userId) => Id == userId;

    public static Result<UserWallet> CreateWallet(Guid ownerId, decimal balance)
    {
        var wallet = new UserWallet(ownerId, balance);
        return WalletValidator.Validator.Validate(wallet);
    }

    public Result ApplyTransaction(Transaction transaction)
    {
        ArgumentNullException.ThrowIfNull(transaction);

        if (IsDeleted)
            return Result.Failure(Error.InvalidData("Wallet has been deleted"));

        if (transaction.WalletAndOwnerId != Id)
            return Result.Failure(Error.InvalidData("Transaction belongs to another wallet"));

        if (transaction.IsOwner(Id) == false)
            return Result.Failure(Error.InvalidData("Transaction belongs to another user"));

        if (transaction.Amount <= 0)
            return Result.Failure(Error.InvalidData("Transaction amount must be positive"));

        switch (transaction.TransactionType)
        {
            case TransactionType.Credit:
                Balance += transaction.Amount;
                break;

            case TransactionType.Debit:
                if (Balance < transaction.Amount)
                    return Result.Failure(Error.InvalidData("Insufficient funds"));

                Balance -= transaction.Amount;
                break;

            default:
                return Result.Failure(Error.InvalidData("Unknown transaction type"));
        }

        return Result.Success();
    }
}