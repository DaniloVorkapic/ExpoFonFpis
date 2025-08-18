using Backend.Commands;
using FluentValidation;

namespace Backend.Validators
{
    public class CreateRegistrationValidator : AbstractValidatorSupportNullReference<CreateRegistrationCommand>
    {

        public CreateRegistrationValidator()
        {
            RuleFor(e => e.registrationDto.FirstName).NotEmpty().WithMessage("First name is required");
            RuleFor(e => e.registrationDto.LastName).NotEmpty().WithMessage("Last name is required");
            RuleFor(e => e.registrationDto.NumberOfPeople).LessThan(6).WithMessage("Number of people need to be less than 6.");
            RuleFor(e => e.registrationDto.Address.StreetOne).NotNull().NotEmpty().WithMessage("Street one is required.");
            RuleFor(e => e.registrationDto.Address.CityName).NotNull().NotEmpty().WithMessage("City is required.");
            RuleFor(e => e.registrationDto.Address.Country).NotNull().NotEmpty().WithMessage("Country is required.");
            RuleFor(e => e.registrationDto.Address.PostCode).NotNull().NotEmpty().WithMessage("Post code is required.");
            RuleFor(e => e.registrationDto.Email).NotNull().NotEmpty().WithMessage("Email is required.");
            RuleFor(e => e.registrationDto).Must(dto => dto.IsPhotoReserved || dto.IsArtReserved).WithMessage("At least one of photo or art must be selected.");
            RuleFor(e => e.registrationDto.Email).Equal(x => x.registrationDto.ConfirmEmail).WithMessage("Email and confirm email need to be equal.");
        }
    }
}
