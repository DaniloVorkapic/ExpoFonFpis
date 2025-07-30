using Backend.DTOs;
using Backend.Http;

namespace Backend.Queries
{
    public record GetAllRegistrationsQuery : BaseQuery<Result<List<RegistrationDto>>>;
}
