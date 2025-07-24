using Backend.DTOs;
using Backend.Http;
using MediatR;

namespace Backend.Queries
{
    public record GetAllNotificationRecipientsQuery : BaseQuery<Result<List<NotificationRecipientDto>>>;
}
