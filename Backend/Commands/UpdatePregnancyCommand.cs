using Backend.DTOs;
using Backend.Http;
using MediatR;

namespace Backend.Commands
{
    public record UpdatePregnancyCommand(UpdatePregnancyDto PregnancyDto) : BaseCommand<Result<PregnancyDto>>;
}
