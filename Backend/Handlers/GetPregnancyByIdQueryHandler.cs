using AutoMapper;
using Backend.DTOs;
using Backend.Entities;
using Backend.Http;
using Backend.Queries;
using Backend.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Backend.Handlers
{
    public class GetPregnancyByIdQueryHandler : BaseQueryHandler<GetPregnancyByIdQuery, Result<PregnancyDto>, Employee>
    {
        public GetPregnancyByIdQueryHandler(IGenericRepository<Employee> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public override async Task<Result<PregnancyDto>> Handle(GetPregnancyByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await Repository.GetQueryable()
                .OfType<FemaleEmployee>()
                .Include(e => e.Pregnancies)
                .ThenInclude(p => p.Leaves)
                .FirstOrDefaultAsync(e => e.Id == request.EmployeeId, cancellationToken);

            if (employee == null)
            {
                return Result<PregnancyDto>.Failure("Employee not found.");
            }

            var pregnancy = employee.Pregnancies.FirstOrDefault(p => p.Id == request.PregnancyId);
            return pregnancy == null ? Result<PregnancyDto>.Failure("Pregnancy not found.") : Result<PregnancyDto>.Success(Mapper.Map<PregnancyDto>(pregnancy));
        }
    }
}
