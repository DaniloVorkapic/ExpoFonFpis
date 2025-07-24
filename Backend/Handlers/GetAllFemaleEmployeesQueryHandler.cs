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
    public class GetAllFemaleEmployeesQueryHandler : BaseQueryHandler<GetAllFemaleEmployeesQuery, Result<List<FemaleEmployeeDto>>, Employee>
    {
        public GetAllFemaleEmployeesQueryHandler(IGenericRepository<Employee> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public override async Task<Result<List<FemaleEmployeeDto>>> Handle(GetAllFemaleEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = await Repository.GetQueryable()
                .OfType<FemaleEmployee>()
                .Include(e => e.Children)
                .Include(e => e.Pregnancies)
                .ToListAsync(cancellationToken);

            return Result<List<FemaleEmployeeDto>>.Success(Mapper.Map<List<FemaleEmployeeDto>>(employees));
        }
    }
}
