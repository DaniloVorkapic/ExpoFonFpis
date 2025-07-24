using Backend.DTOs;
using Backend.Http;
using MediatR;

namespace Backend.Commands
{
    public record CreatePregnancyCommand(CreatePregnancyDto PregnancyDto) : BaseCommand<Result<EmployeeDto>>;
}
