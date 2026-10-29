namespace InventoryService.Features.Product.Dtos;

public class ProductDto
{
    public Guid Id { get; init; }
    public DateTime CreatedAtUtc { get; init; }

    public DateTime? DeletedAtUtc { get; init; }
    public bool IsDeleted { get; init; }


    public required string Name { get; init; }
    public required string Description { get; init; }
    
    public decimal SumForOne { get; init; }
    public long Quantity { get; init; }
    public Guid OwnerId { get; init; }
}