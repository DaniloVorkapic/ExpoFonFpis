using Backend.Events;
using Backend.Services;
using Hangfire;
using MediatR;

namespace Backend.Handlers
{
    public class HolidayUpdatedEventHandler : INotificationHandler<HolidayUpdatedEvent>
    {
        public Task Handle(HolidayUpdatedEvent notification, CancellationToken cancellationToken)
        {
            BackgroundJob.Enqueue<IReturnDateService>(x => x.RecalculateReturnDateForAllEmployees());
            return Task.CompletedTask;
        }
    }
}
