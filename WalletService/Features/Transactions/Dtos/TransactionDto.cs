namespace WalletService.Features.Transactions.Dtos;

public class TransactionDto
{
    public Guid Id { get; init; }
    public DateTime CreatedAtUtc { get; init; }

    public DateTime? DeletedAtUtc { get; init; }
    public bool IsDeleted { get; init; }
    
    public decimal Amount { get; init; }
    public required string TransactionType { get; init; }
    public Guid WalletAndOwnerId { get; init; }
}