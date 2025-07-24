using Backend.DTOs;
using Backend.Http;
using MediatR;

namespace Backend.Queries
{
    public record GetEmployeeByIdQuery(long EmployeeId) : BaseQuery<Result<EmployeeDto>>;
}
