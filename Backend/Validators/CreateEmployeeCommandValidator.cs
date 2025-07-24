using Backend.Commands;
using FluentValidation;

namespace Backend.Validators
{
    public class CreateEmployeeCommandValidator : AbstractValidatorSupportNullReference<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator()
        {
            RuleFor(e => e.EmployeeDto.FirstName).NotEmpty().WithMessage("First name is required");
            RuleFor(e => e.EmployeeDto.LastName).NotEmpty().WithMessage("Last name is required");
        }
    }
}
