using Shared;
using Shared.Mediator;
using WalletService.Features.Transactions.Abstractions;

namespace WalletService.Features.Transactions.CreateTransaction;

public class CreateTransactionHandler(IPaymentService paymentService)
    : ICommandHandler<CreateTransactionCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        var result = await paymentService.CreditOrTakeMoney(
            request.WalletAndOwnerId,
            request.Amount,
            request.TransactionType,
            cancellationToken);

        return result.IsSuccess
            ? Result<Guid>.Success(result.Value)
            : Result<Guid>.Failure(result.Error!);
    }
}