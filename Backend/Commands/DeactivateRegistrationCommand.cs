using Backend.DTOs;
using Backend.Http;

namespace Backend.Commands
{
    public record DeactivateRegistrationCommand(DeactivateRegistrationDto registrationDto) : BaseCommand<Result<DeactivateRegistrationResponse>>;
}
