using Backend.Commands;
using FluentValidation;

namespace Backend.Validators
{
    public class CreateLeaveCommandValidator : AbstractValidatorSupportNullReference<CreateLeaveCommand>
    {
        public CreateLeaveCommandValidator()
        {
            RuleFor(l => l.LeaveDto.LeaveType).IsInEnum()
                .WithMessage("Leave type can be either Godisnji, Porodiljsko, Nega Deteta or Ostalo");
            RuleFor(l => l.LeaveDto.DurationType).IsInEnum()
                .WithMessage("Duration type can be either Days, Weeks or Months");
            RuleFor(l => l.LeaveDto.Duration)
                .GreaterThan(0).WithMessage("Duration of Leave has to be a positive number");
        }
    }
}
