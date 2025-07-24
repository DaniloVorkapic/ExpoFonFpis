using Backend.DTOs;
using Backend.Http;
using MediatR;

namespace Backend.Queries
{
    public record GetAllChildrenQuery(long EmployeeId) : BaseQuery<Result<List<ChildDto>>>;
}
