using Shared.Validating;

namespace InventoryService.Features.Product.Validators;

public class ProductValidator : BaseValidator<Product>
{
    public ProductValidator()
    {
        
    }
    
    public static readonly ProductValidator Validator = new();
}