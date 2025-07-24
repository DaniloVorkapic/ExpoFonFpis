using Backend.Commands;
using Backend.Entities;
using Backend.Http;
using Backend.Repositories;
using MediatR;

namespace Backend.Handlers
{
    public class DeleteNotificationRecipientCommandHandler : BaseCommandHandler<DeleteNotificationRecipientCommand, Result<Unit>, NotificationRecipient>
    {
        public DeleteNotificationRecipientCommandHandler(IGenericRepository<NotificationRecipient> repository,
            IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }

        public override async Task<Result<Unit>> Handle(DeleteNotificationRecipientCommand request, CancellationToken cancellationToken)
        {
            var notificationRecipient = await Repository.GetByIdAsync(request.Id);
            if (notificationRecipient == null)
            {
                return Result<Unit>.Failure("Notification recipient not found.");
            }

            await Repository.DeleteAsync(notificationRecipient);
            await UnitOfWork.CommitAsync();

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
