using Backend.Commands;
using FluentValidation;

namespace Backend.Validators
{
    public class CreateHolidayCommandValidator : AbstractValidatorSupportNullReference<CreateHolidayCommand>
    {
        public CreateHolidayCommandValidator()
        {
            RuleFor(h => h.HolidayDto.Description).NotEmpty().WithMessage("Description of a Holiday is required");
        }
    }
}
