using Backend.DTOs;
using Backend.Http;
using MediatR;

namespace Backend.Commands
{
    public record CreateEmployeeCommand(CreateEmployeeDto EmployeeDto) : BaseCommand<Result<EmployeeDto>>;
}
