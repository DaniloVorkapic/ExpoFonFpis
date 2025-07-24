using Backend.DTOs;
using Backend.Http;
using MediatR;

namespace Backend.Queries
{
    public record GetAllPregnanciesQuery(long EmployeeId) : BaseQuery<Result<List<PregnancyDto>>>;
}
