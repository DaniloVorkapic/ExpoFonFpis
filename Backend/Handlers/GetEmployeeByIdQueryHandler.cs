using AutoMapper;
using Backend.DTOs;
using Backend.Entities;
using Backend.Http;
using Backend.Queries;
using Backend.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Backend.Handlers
{
    public class GetEmployeeByIdQueryHandler : BaseQueryHandler<GetEmployeeByIdQuery, Result<EmployeeDto>, Employee>
    {
        public GetEmployeeByIdQueryHandler(IGenericRepository<Employee> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public override async Task<Result<EmployeeDto>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            EmployeeDto employeeDto;

            var femaleEmployee = await Repository.GetQueryable()
                .OfType<FemaleEmployee>()
                .FirstOrDefaultAsync(e => e.Id == request.EmployeeId, cancellationToken);

            if (femaleEmployee != null)
            {
                await Repository.GetQueryable()
                    .OfType<FemaleEmployee>()
                    .Include(f => f.Pregnancies)
                    .Include(f => f.Children)
                    .ToListAsync(cancellationToken);

                employeeDto = Mapper.Map<FemaleEmployeeDto>(femaleEmployee);
            }
            else
            {
                var maleEmployee = await Repository.GetByIdAsync(request.EmployeeId);
                if (maleEmployee != null)
                {
                    await Repository.GetQueryable()
                        .OfType<MaleEmployee>()
                        .Include(m => m.Children)
                        .ToListAsync(cancellationToken);
                    employeeDto = Mapper.Map<MaleEmployeeDto>(maleEmployee);
                }
                else
                {
                    return Result<EmployeeDto>.Failure("Employee not found.");
                }
            }

            return Result<EmployeeDto>.Success(employeeDto);
        }
    }
}
