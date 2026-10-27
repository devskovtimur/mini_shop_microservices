using InventoryService.Features.Product.Validators;
using Shared;

namespace InventoryService.Features.Product;

public class Product : BaseEntity
{
    private Product(string name, string description, decimal sumForOne, long quantity, Guid ownerId)
    {
        Name = name;
        Description = description;
        SumForOne = sumForOne;
        Quantity = quantity;
        OwnerId = ownerId;
    }

    public string Name { get; init; }
    public string Description { get; init; }
    public decimal SumForOne { get; init; }
    public long Quantity { get; private set; }
    public Guid OwnerId { get; init; }

    // public decimal WeightInMilligrams { get; init; }
    // TODO: Add that property + a ProductState property to use shipping price when product is checked and ready to be purchased

    public Result<Product> CreateProduct(string name, string description, decimal sumForOne, long quantity,
        Guid ownerId)
    {
        var product = new Product(name, description, sumForOne, quantity, ownerId);
        return ProductValidator.Validator.Validate(product);
    }

    public void AddProducts(long newProductsQuantity)
    {
        Quantity += newProductsQuantity;
    }

    // public Result<decimal> TakeProductsAndGetTotalSum(long takenQuantity, decimal priceForShippingOfOne)
    // {
    //     var totalSumResult = CalculateSum(takenQuantity, priceForShippingOfOne);
    //
    //     if (totalSumResult.IsFailure)
    //     {
    //         return Result<decimal>.Failure(totalSumResult.Error!);
    //     }
    //
    //     Quantity -= takenQuantity;
    //     return Result<decimal>.Success(totalSumResult.Value);
    // }

    public Result<decimal> CalculateSum(long takenQuantity, decimal priceForShippingOfOne)
    {
        if (takenQuantity <= 0)
        {
            return Result<decimal>.Failure(Error.InvalidData("Quantity must be greater than zero"));
        }

        if (priceForShippingOfOne <= 0)
        {
            return Result<decimal>.Failure(Error.InvalidData("Price For Shipping must be greater than zero"));
        }

        if (HasEspecialQuantity(takenQuantity))
        {
            return Result<decimal>.Success(SumForOne * takenQuantity * priceForShippingOfOne);
        }

        return Result<decimal>.Failure(
            Error.InvalidData($"Quantity {takenQuantity} is greater than the product quantity {Quantity}"));
    }

    public bool HasEspecialQuantity(long takenQuantity)
    {
        return takenQuantity <= Quantity;
    }

    public Result Reserve(long quantity)
    {
        if (quantity <= 0)
        {
            return Result.Failure(
                Error.InvalidData("Quantity must be positive"));
        }

        if (Quantity < quantity)
        {
            return Result.Failure(
                Error.InvalidData("Insufficient quantity"));
        }

        Quantity -= quantity;
        return Result.Success();
    }
}