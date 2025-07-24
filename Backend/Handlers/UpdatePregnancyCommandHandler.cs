using AutoMapper;
using Backend.Commands;
using Backend.DTOs;
using Backend.Entities;
using Backend.Http;
using Backend.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Backend.Handlers
{
    public class UpdatePregnancyCommandHandler : BaseCommandHandler<UpdatePregnancyCommand, Result<PregnancyDto>, Employee>
    {
        private readonly IMapper _mapper;

        public UpdatePregnancyCommandHandler(IGenericRepository<Employee> repository, IUnitOfWork unitOfWork, IMapper mapper) : base(repository, unitOfWork)
        {
            _mapper = mapper;
        }

        public override async Task<Result<PregnancyDto>> Handle(UpdatePregnancyCommand request, CancellationToken cancellationToken)
        {
            var employee = await Repository.GetQueryable()
                .OfType<FemaleEmployee>()
                .Include(e => e.Pregnancies)
                .FirstOrDefaultAsync(e => e.Id == request.PregnancyDto.EmployeeId, cancellationToken);

            if (employee == null)
            {
                return Result<PregnancyDto>.Failure("Employee not found");
            }

            var pregnancy = employee.Pregnancies.FirstOrDefault(p => p.Id == request.PregnancyDto.PregnancyId);
            if (pregnancy == null)
            {
                return Result<PregnancyDto>.Failure("Pregnancy not found");
            }

            pregnancy.Update(request.PregnancyDto.DateOfOpeningPregnancy);
            await UnitOfWork.CommitAsync();

            return Result<PregnancyDto>.Success(_mapper.Map<PregnancyDto>(pregnancy));
        }
    }

}
