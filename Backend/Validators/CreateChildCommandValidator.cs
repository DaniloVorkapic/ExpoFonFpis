using Backend.Commands;
using FluentValidation;

namespace Backend.Validators
{
    public class CreateChildCommandValidator : AbstractValidatorSupportNullReference<CreateChildCommand>
    {
        public CreateChildCommandValidator()
        {
            RuleFor(c => c.ChildDto.FirstName).NotEmpty().WithMessage("First name is required");
            RuleFor(c => c.ChildDto.LastName).NotEmpty().WithMessage("Last name is required");
        }
    }
}
