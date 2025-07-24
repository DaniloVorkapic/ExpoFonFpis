using FluentValidation;
using FluentValidation.Results;

namespace Backend.Validators
{
    public abstract class AbstractValidatorSupportNullReference<T> : AbstractValidator<T>
    {
        protected override bool PreValidate(ValidationContext<T> context, ValidationResult result)
        {
            if (context.InstanceToValidate == null)
            {
                result.Errors.Add(new ValidationFailure("", $"Instance of {typeof(T).Name} cannot be nll") {ErrorCode = "BAD_REQUEST"});
                return false;
            }

            return true;
        }
    }
}
