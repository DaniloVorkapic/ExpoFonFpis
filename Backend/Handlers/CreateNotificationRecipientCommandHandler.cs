using System.ComponentModel.DataAnnotations;
using Backend.Commands;
using Backend.Entities;
using Backend.Http;
using Backend.Repositories;
using Backend.DTOs;
using FluentValidation;
using MediatR;

namespace Backend.Handlers
{
    public class CreateNotificationRecipientCommandHandler : BaseCommandHandler<CreateNotificationRecipientCommand, Result<Unit>, NotificationRecipient>
    {
        private readonly IValidator<CreateNotificationRecipientCommand> _validator;

        public CreateNotificationRecipientCommandHandler(IGenericRepository<NotificationRecipient> repository, IUnitOfWork unitOfWork, IValidator<CreateNotificationRecipientCommand> validator)
            : base(repository, unitOfWork)
        {
            _validator = validator;
        }

        public override async Task<Result<Unit>> Handle(CreateNotificationRecipientCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            var notificationRecipient = NotificationRecipient.Create(request.NotificationRecipientDto.Email);
            
            await Repository.CreateAsync(notificationRecipient);
            await UnitOfWork.CommitAsync();

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
