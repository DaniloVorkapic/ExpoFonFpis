using Backend.DTOs;
using Backend.Http;

namespace Backend.Queries
{
    public record GetPricesQuery : BaseQuery<Result<PricesDto>>;
}
