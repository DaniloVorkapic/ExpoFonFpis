using Backend.DTOs;
using Backend.Http;
using MediatR;

namespace Backend.Queries
{
    public record GetAllFemaleEmployeesQuery() : BaseQuery<Result<List<FemaleEmployeeDto>>>;
}
