using AutoMapper;
using Backend.DTOs;
using Backend.Entities;
using Backend.Http;
using Backend.Queries;
using Backend.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Backend.Handlers
{
    public class GetAllPregnanciesQueryHandler : BaseQueryHandler<GetAllPregnanciesQuery, Result<List<PregnancyDto>>, Employee>
    {
        public GetAllPregnanciesQueryHandler(IGenericRepository<Employee> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public override async Task<Result<List<PregnancyDto>>> Handle(GetAllPregnanciesQuery request, CancellationToken cancellationToken)
        {
            var employee = await Repository.GetQueryable()
                .OfType<FemaleEmployee>()
                .Include(e => e.Pregnancies)
                .ThenInclude(p => p.Leaves)
                .FirstOrDefaultAsync(e => e.Id == request.EmployeeId, cancellationToken);

            if (employee == null)
            {
                return Result<List<PregnancyDto>>.Failure("Employee not found.");
            }

            var pregnanciesDto = employee.Pregnancies.Select(p => Mapper.Map<PregnancyDto>(p)).ToList();
            return Result<List<PregnancyDto>>.Success(pregnanciesDto);
        }
    }
}
