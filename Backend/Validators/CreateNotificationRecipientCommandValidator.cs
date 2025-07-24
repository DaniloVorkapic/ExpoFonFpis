using Backend.Commands;
using Backend.Entities;
using Backend.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Backend.Validators
{
    public class CreateNotificationRecipientCommandValidator : AbstractValidatorSupportNullReference<CreateNotificationRecipientCommand>
    {
        public CreateNotificationRecipientCommandValidator(IGenericRepository<NotificationRecipient> repository)
        {
            RuleFor(n => n.NotificationRecipientDto.Email)
                .NotEmpty()
                .WithMessage("Email is required");

            RuleFor(n => n.NotificationRecipientDto.Email)
                .MustAsync(async (email, _) => await IsEmailUnique(repository, email))
                .WithMessage("Email must be unique");
        }

        private static async Task<bool> IsEmailUnique(IGenericRepository<NotificationRecipient> repository, string email)
        {
            return !await repository.GetQueryable().AnyAsync(n => n.Email == email);
        }
    }
}
