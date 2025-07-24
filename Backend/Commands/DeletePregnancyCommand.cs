using Backend.Http;
using MediatR;

namespace Backend.Commands
{
    public record DeletePregnancyCommand(long EmployeeId, long PregnancyId) : BaseCommand<Result<Unit>>;
}
