using Backend.DTOs;
using Backend.Http;
using MediatR;

namespace Backend.Queries
{
    public record GetAllEmployeesQuery(string? NameFilter) : BaseQuery<Result<List<EmployeeDto>>>;
}
