using Backend.DTOs;
using Backend.Http;
using MediatR;

namespace Backend.Commands
{
    public record CreateNotificationRecipientCommand(CreateNotificationRecipientDto NotificationRecipientDto) : BaseCommand<Result<Unit>>;
}
