using AutoMapper;
using Backend.DTOs;
using Backend.Entities;
using Backend.Http;
using Backend.Queries;
using Backend.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Backend.Handlers
{
    public class GetAllChildrenQueryHandler : BaseQueryHandler<GetAllChildrenQuery, Result<List<ChildDto>>, Employee>
    {
        public GetAllChildrenQueryHandler(IGenericRepository<Employee> employeeRepository, IMapper mapper) : base(
            employeeRepository, mapper)
        {
        }

        public override async Task<Result<List<ChildDto>>> Handle(GetAllChildrenQuery request, CancellationToken cancellationToken)
        {
            var employee = await Repository.GetQueryable()
                .Include(e => e.Children)
                .FirstOrDefaultAsync(e => e.Id == request.EmployeeId, cancellationToken);

            if (employee == null)
            {
                return Result<List<ChildDto>>.Failure("Employee not found.");
            }

            var childrenDto = employee.Children.Select(c => Mapper.Map<ChildDto>(c)).ToList();
            return Result<List<ChildDto>>.Success(childrenDto);
        }
    }
}
