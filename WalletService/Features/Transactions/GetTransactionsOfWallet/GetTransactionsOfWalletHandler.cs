using AutoMapper;
using Shared;
using Shared.Mediator;
using WalletService.Features.Transactions.Abstractions;
using WalletService.Features.Transactions.Dtos;

namespace WalletService.Features.Transactions.GetTransactionsOfWallet;

public class GetTransactionsOfWalletHandler(ITransactionRepository transactionRepository, IMapper mapper)
    : IQueryHandler<GetTransactionsOfWalletQuery, IEnumerable<TransactionDto>>
{
    public async Task<Result<IEnumerable<TransactionDto>>> Handle(GetTransactionsOfWalletQuery request,
        CancellationToken cancellationToken)
    {
        var transactions = await transactionRepository.GetTransactions(
            request.WalletId,
            request.PageSize,
            request.Page,
            ct: cancellationToken);

        var dtos = transactions.Select(mapper.Map<TransactionDto>);
        return Result<IEnumerable<TransactionDto>>.Success(dtos);
    }
}