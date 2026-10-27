using FluentValidation;

namespace Shared.Validating;

public abstract class BaseValidator<T> : AbstractValidator<T> where T : notnull
{
    public new Result<T> Validate(T objectToValidate)
    {
        var result = base.Validate(objectToValidate);

        return result.IsValid
            ? Result<T>.Success(objectToValidate)
            : Result<T>.Failure(
                Error.InvalidData(result.Errors.First().ErrorMessage));
    }
}