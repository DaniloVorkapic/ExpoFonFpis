using AutoMapper;
using Backend.Commands;
using Backend.DTOs;
using Backend.Entities;
using Backend.Events;
using Backend.Http;
using Backend.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Backend.Handlers
{
    public class CreatePregnancyCommandHandler : BaseCommandHandler<CreatePregnancyCommand, Result<EmployeeDto>, Employee>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CreatePregnancyCommandHandler(IGenericRepository<Employee> repository, IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator)
            : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public override async Task<Result<EmployeeDto>> Handle(CreatePregnancyCommand request, CancellationToken cancellationToken)
        {
            var employee = await Repository.GetQueryable()
                .OfType<FemaleEmployee>()
                .Include(e => e.Pregnancies)
                .FirstOrDefaultAsync(e => e.Id == request.PregnancyDto.EmployeeId, cancellationToken);

            if (employee == null)
            {
                return Result<EmployeeDto>.Failure("Employee not found.");
            }

            var pregnancy = Pregnancy.Create(request.PregnancyDto.DateOfOpeningPregnancy,
                request.PregnancyDto.DateOfChildbirth);

            employee.Pregnancies.Add(pregnancy);

            await UnitOfWork.CommitAsync();
            await _mediator.Publish(new EmployeeUpdatedEvent(employee.Id), cancellationToken);

            return Result<EmployeeDto>.Success(_mapper.Map<EmployeeDto>(employee));
        }
    }
}
