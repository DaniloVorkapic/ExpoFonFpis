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
    public class GetAllMaleEmployeesQueryHandler : BaseQueryHandler<GetAllMaleEmployeesQuery, Result<List<MaleEmployeeDto>>, Employee>
    {
        public GetAllMaleEmployeesQueryHandler(IGenericRepository<Employee> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public override async Task<Result<List<MaleEmployeeDto>>> Handle(GetAllMaleEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = await Repository.GetQueryable()
                .OfType<MaleEmployee>()
                .Include(e => e.Children)
                .ToListAsync(cancellationToken);

            return Result<List<MaleEmployeeDto>>.Success(Mapper.Map<List<MaleEmployeeDto>>(employees));
        }
    }
}
