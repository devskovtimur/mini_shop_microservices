using InventoryService.Features.Product.Validators;
using Shared;

namespace InventoryService.Features.Product;

public class DescriptionValueObject
{
    private DescriptionValueObject(string description)
    {
        Description = description;
    }
    public string Description { get; init; }
    

    public static Result<DescriptionValueObject> Create(string description)
    {
        var valueObject = new DescriptionValueObject(description);
        return DescriptionValidator.Validator.Validate(valueObject);
    }

    public override string ToString()
    {
        return Description;
    }
}