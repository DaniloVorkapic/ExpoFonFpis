using Backend.Http;
using MediatR;

namespace Backend.Commands
{
    public record DeleteChildCommand(long EmployeeId, long ChildId) : BaseCommand<Result<Unit>>;
}
