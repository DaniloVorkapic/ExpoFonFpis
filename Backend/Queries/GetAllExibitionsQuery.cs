using Backend.DTOs;
using Backend.Http;

namespace Backend.Queries
{
    public record GetAllExibitionsQuery(long ExibitionType) : BaseQuery<Result<List<ExibitionDto>>>;
}
