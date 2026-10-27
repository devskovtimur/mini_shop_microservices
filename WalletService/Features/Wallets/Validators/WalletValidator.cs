using FluentValidation;
using Shared.Validating;

namespace WalletService.Features.Wallets.Validators;

public class WalletValidator : BaseValidator<UserWallet>
{
    private WalletValidator()
    {
        RuleFor(x => x.Id).NotEmpty().Must(x => x != Guid.Empty).WithMessage("Wallet id must be set");
        RuleFor(x => x.Balance).Must(x => x >= 0).WithMessage("Balance must be greater or equals than 0");
    }
    
    public static readonly WalletValidator Validator = new();
}