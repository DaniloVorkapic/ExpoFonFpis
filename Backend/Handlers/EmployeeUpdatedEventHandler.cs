using Backend.Events;
using Backend.Services;
using Hangfire;
using MediatR;

namespace Backend.Handlers
{
    public class EmployeeUpdatedEventHandler : INotificationHandler<EmployeeUpdatedEvent>
    {
        public Task Handle(EmployeeUpdatedEvent notification, CancellationToken cancellationToken)
        {
            BackgroundJob.Enqueue<IReturnDateService>(x => x.RecalculateReturnDateForEmployee(notification.EmployeeId));
            return Task.CompletedTask;
        }
    }
}
