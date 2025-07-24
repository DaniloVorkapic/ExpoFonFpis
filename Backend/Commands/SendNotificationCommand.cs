using Backend.Http;
using MediatR;

namespace Backend.Commands
{
    public record SendNotificationCommand : INotification;
}
