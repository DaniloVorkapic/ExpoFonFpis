using Backend.Commands;
using Backend.DTOs;
using Backend.Queries;
using Backend.Utils;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/notification-recipients")]
    [ApiController]
    public class NotificationRecipientsController : BaseController
    {
        public NotificationRecipientsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNotificationRecipients()
        {
            return ResultHandler.HandleResult(await Mediator.Send(new GetAllNotificationRecipientsQuery()));
        }

        [HttpPost]
        public async Task<IActionResult> CreateNotificationRecipient([FromBody] CreateNotificationRecipientDto notificationRecipientDto)
        {
            return ResultHandler.HandleResult(await Mediator.Send(new CreateNotificationRecipientCommand(notificationRecipientDto)));
        }

        [HttpDelete("{notificationRecipientId:long}")]
        public async Task<IActionResult> DeleteNotificationRecipient([FromRoute] long notificationRecipientId)
        {
            return ResultHandler.HandleResult(await Mediator.Send(new DeleteNotificationRecipientCommand(notificationRecipientId)));
        }
       
    }
}
