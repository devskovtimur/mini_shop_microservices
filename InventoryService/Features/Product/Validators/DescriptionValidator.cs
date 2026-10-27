using FluentValidation;
using Shared.Validating;

namespace InventoryService.Features.Product.Validators;

public class DescriptionValidator : BaseValidator<DescriptionValueObject>
{
    public const int MaxDescriptionLength = 5000;
    public const int MinDescriptionLength = 3;

    private DescriptionValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty().NotNull().WithMessage("Description is empty")
            .MinimumLength(MinDescriptionLength)
            .MaximumLength(MaxDescriptionLength)
            .WithMessage($"Description length must be between {MinDescriptionLength} and {MaxDescriptionLength}");
    }

    public static readonly DescriptionValidator Validator = new();
}