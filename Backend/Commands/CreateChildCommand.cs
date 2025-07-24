using Backend.DTOs;
using Backend.Http;
using MediatR;

namespace Backend.Commands
{
    public record CreateChildCommand(CreateChildDto ChildDto) : BaseCommand<Result<EmployeeDto>>;
}
