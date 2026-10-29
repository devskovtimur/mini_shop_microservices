using System.ComponentModel.DataAnnotations;

namespace WalletService.Features.Transactions.CreateTransaction;

public record TakeMoneyRequest(
    [Required, Range(0, double.MaxValue)] decimal Amount);