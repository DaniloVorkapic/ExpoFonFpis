using Backend.Http;
using MediatR;

namespace Backend.Commands
{
    public record DeleteNotificationRecipientCommand(long Id) : BaseCommand<Result<Unit>>;
}
