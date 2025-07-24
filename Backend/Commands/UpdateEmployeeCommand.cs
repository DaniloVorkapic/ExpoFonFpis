using Backend.DTOs;
using Backend.Http;
using MediatR;

namespace Backend.Commands
{
    public record UpdateEmployeeCommand(UpdateEmployeeDto EmployeeDto) : BaseCommand<Result<EmployeeDto>>;
}
