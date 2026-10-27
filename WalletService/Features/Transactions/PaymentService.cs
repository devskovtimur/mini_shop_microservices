using Shared;
using Shared.Abstractions;
using WalletService.Features.Transactions.Abstractions;
using WalletService.Features.Wallets.Abstractions;
using WalletService.Shared.Infrastructure.Abstractions;

namespace WalletService.Features.Transactions;

public class PaymentService(
    IWalletRepository walletRepository,
    ITransactionRepository transactionRepository,
    IUnitOfWorkProvider unitOfWorkProvider) : IPaymentService
{
    public async Task<Result<Guid>> CreditMoney(Guid walletAndOwnerId, decimal amount, CancellationToken ct = default)
    {
        return await CreditOrTakeMoney(walletAndOwnerId, amount, TransactionType.Credit, ct);
    }

    public async Task<Result<Guid>> TakeMoney(Guid walletAndOwnerId, decimal amount, CancellationToken ct = default)
    {
        return await CreditOrTakeMoney(walletAndOwnerId, amount, TransactionType.Debit, ct);
    }

    public async Task<Result<Guid>> CreditOrTakeMoney(Guid walletAndOwnerId, decimal amount,
        TransactionType transactionType,
        CancellationToken ct = default)
    {
        // 1) Create UnitOfWork. Repositories have the same UnitOfWorkContext,
        // so it's possible to use the same DbTransaction and Connection (Scoped dependency) 
        var unitOfWork = await unitOfWorkProvider.Create(ct);
        await unitOfWork.BeginTransaction(ct);

        try
        {
            // 2) Check if the wallet exists  
            var wallet = await walletRepository.GetById(walletAndOwnerId, ct: ct);

            if (wallet is null)
            {
                await unitOfWork.RollbackTransaction(ct);

                return Result<Guid>.Failure(
                    Error.NotFound($"User and its Wallet with {walletAndOwnerId} ID were not found"));
            }

            // 3) Create a Transaction to take or credit money
            var creatingResult = Transaction.CreateTransaction(amount, walletAndOwnerId, transactionType);

            if (creatingResult.IsFailure)
            {
                await unitOfWork.RollbackTransaction(ct);
                return Result<Guid>.Failure(creatingResult.Error!);
            }

            var transaction = creatingResult.Value!;

            // 4) ApplyTransaction is called here to take/credit money if it's possible, otherwise this method returns fail
            var transactionApplyingResult = wallet.ApplyTransaction(transaction);

            if (transactionApplyingResult.IsFailure)
            {
                await unitOfWork.RollbackTransaction(ct);
                return Result<Guid>.Failure(transactionApplyingResult.Error!);
            }

            // 5) Now, Saving changes 
            await walletRepository.UpdateBalance(wallet, ct: ct);
            await transactionRepository.Add(transaction, ct: ct);

            await unitOfWork.CommitTransaction(ct);
            return Result<Guid>.Success(transaction.Id);
        }
        catch
        {
            await unitOfWork.RollbackTransaction(ct);
            throw;
        }
    }
}