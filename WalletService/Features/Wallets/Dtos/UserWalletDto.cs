namespace WalletService.Features.Wallets.Dtos;

public record UserWalletDto
{
    public Guid Id { get; init; }
    public DateTime CreatedAtUtc { get; init; }

    public DateTime? DeletedAtUtc { get; init; }
    public bool IsDeleted { get; init; }

    public decimal Balance { get; init; }
}