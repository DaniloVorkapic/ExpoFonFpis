using MediatR;

namespace Backend.Events
{
    public class EmployeeUpdatedEvent : INotification
    {
        public long EmployeeId { get; }

        public EmployeeUpdatedEvent(long employeeId)
        {
            EmployeeId = employeeId;
        } 
    }
}
