using Backend.DTOs;
using Backend.Http;

namespace Backend.Queries
{
    public record GetConfimrationQuery(GetConfirmationDto confirmationDto) : BaseQuery<Result<ConfirmationResponse>>;
}
