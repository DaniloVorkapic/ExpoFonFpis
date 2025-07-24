using Backend.Commands;
using Backend.Services;
using Hangfire;
using MediatR;

namespace Backend.Handlers
{
    public class SendNotificationCommandHandler : INotificationHandler<SendNotificationCommand>
    {
        private readonly INotificationService _notificationService;

        public SendNotificationCommandHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public Task Handle(SendNotificationCommand request, CancellationToken cancellationToken)
        {
            RecurringJob.AddOrUpdate("send-notification",() => _notificationService.SendEmailToRecipients(), Cron.Daily);
            return Task.CompletedTask;
        }

    }
}
