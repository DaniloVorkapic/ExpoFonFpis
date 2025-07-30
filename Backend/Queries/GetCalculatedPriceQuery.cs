using Backend.DTOs;
using Backend.Http;

namespace Backend.Queries
{
    public record GetCalculatedPriceQuery(GetCalculatedDto calculatedDto) : BaseQuery<Result<CalculatedDto>>;
}
