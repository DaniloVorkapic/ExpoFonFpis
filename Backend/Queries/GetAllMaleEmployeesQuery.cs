using Backend.DTOs;
using Backend.Http;
using MediatR;

namespace Backend.Queries
{
    public record GetAllMaleEmployeesQuery() : BaseQuery<Result<List<MaleEmployeeDto>>>;
}
