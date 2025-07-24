using Backend.DTOs;
using Backend.Http;
using MediatR;

namespace Backend.Commands
{
    public record CreateLeaveCommand(CreateLeaveDto LeaveDto) : BaseCommand<Result<PregnancyDto>>;
}
