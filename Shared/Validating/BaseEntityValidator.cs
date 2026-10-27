using FluentValidation;

namespace Shared.Validating;

public abstract class BaseEntityValidator<T> : BaseValidator<T> where T : BaseEntity
{
    protected BaseEntityValidator()
    {
        RuleFor(x => x.Id).NotEmpty().Must(x => x != Guid.Empty).WithMessage("Id must be set");

        RuleFor(x => x.CreatedAtUtc).NotNull().WithMessage("CreatedAtUtc must not be null")
            .Must(x => x <= DateTime.UtcNow).WithMessage("CreatedAtUtc must be in the past");

        When(x => x.DeletedAtUtc != null,
            () =>
            {
                RuleFor(x => x.DeletedAtUtc).Must(x => x <= DateTime.UtcNow)
                    .WithMessage("DeletedAtUtc must be in the past");
            });
    }
}