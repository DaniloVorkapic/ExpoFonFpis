using Backend.DTOs;
using Backend.Http;
using MediatR;

namespace Backend.Queries
{
    public record GetPregnancyByIdQuery(long EmployeeId, long PregnancyId) : BaseQuery<Result<PregnancyDto>>;
}
