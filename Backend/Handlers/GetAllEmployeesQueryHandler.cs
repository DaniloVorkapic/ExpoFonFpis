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
    public class GetAllEmployeesQueryHandler : BaseQueryHandler<GetAllEmployeesQuery, Result<List<EmployeeDto>>, Employee>
    {
        public GetAllEmployeesQueryHandler(IGenericRepository<Employee> employeeRepository, IMapper mapper) : base(
            employeeRepository, mapper)
        {
        }

        public override async Task<Result<List<EmployeeDto>>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employeesQuery = Repository.GetQueryable();

            if (!string.IsNullOrEmpty(request.NameFilter))
            {
                var filter = request.NameFilter.ToLower();
                var words = filter.Split(" ");

                employeesQuery = words.Aggregate(employeesQuery, (current, word) =>
                    current.Where(e => e.FirstName.ToLower().Contains(word) || e.LastName.ToLower().Contains(word)));
            }

            var employees = await employeesQuery.Include(e => e.Children).ToListAsync(cancellationToken);

            return Result<List<EmployeeDto>>.Success(Mapper.Map<List<EmployeeDto>>(employees));
        }
    }
}
