using Backend.Http;
using MediatR;

namespace Backend.Commands
{
    public record DeleteLeaveCommand(long EmployeeId, long PregnancyId, long LeaveId) : BaseCommand<Result<Unit>>;
}
