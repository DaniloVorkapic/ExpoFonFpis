using Backend.DTOs;
using Backend.Http;

namespace Backend.Commands
{
    public record CreateRegistrationCommand(CreateRegistrationDto registrationDto) : BaseCommand<Result<RegistrationResponse>>;
}
