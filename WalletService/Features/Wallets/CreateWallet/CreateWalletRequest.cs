using System.ComponentModel.DataAnnotations;

namespace WalletService.Features.Wallets.CreateWallet;

public record CreateWalletRequest(
    [Required, Range(0, double.MaxValue)] decimal DefaultBalance);