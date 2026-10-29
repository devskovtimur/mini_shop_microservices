using System.ComponentModel.DataAnnotations;

namespace WalletService.Features.Transactions.CreateTransaction;

public record CreditMoneyRequest(
    [Required, Range(0, double.MaxValue)] decimal Amount);