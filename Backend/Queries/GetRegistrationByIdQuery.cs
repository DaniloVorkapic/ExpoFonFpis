using Backend.DTOs;
using Backend.Http;

namespace Backend.Queries
{
    public record GetRegistrationByIdQuery(long Id) : BaseQuery<Result<RegistrationByIdDto>>;
}
