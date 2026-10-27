using Shared.Validating;

namespace WalletService.Features.Transactions.Validators;

public class TransactionValidator : BaseValidator<Transaction>
{
    public TransactionValidator()
    {
        
    }
    
    public static readonly TransactionValidator Validator = new();
}