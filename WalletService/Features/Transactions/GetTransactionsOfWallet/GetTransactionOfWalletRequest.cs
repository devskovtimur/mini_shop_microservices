using System.ComponentModel.DataAnnotations;

namespace WalletService.Features.Transactions.GetTransactionsOfWallet;

public record GetTransactionOfWalletRequest(
    [Required, Range(1, int.MaxValue)] int PageSize,
    [Required, Range(1, int.MaxValue)] int Page);