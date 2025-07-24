using AutoMapper;
using Backend.Commands;
using Backend.DTOs;
using Backend.Entities;
using Backend.Events;
using Backend.Http;
using Backend.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Backend.Handlers
{
    public class CreateLeaveCommandHandler : BaseCommandHandler<CreateLeaveCommand, Result<PregnancyDto>, Employee>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IValidator<CreateLeaveCommand> _validator;

        public CreateLeaveCommandHandler(IGenericRepository<Employee> repository, IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator, IValidator<CreateLeaveCommand> validator)
            : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _mediator = mediator;
            _validator = validator;
        }

        public override async Task<Result<PregnancyDto>> Handle(CreateLeaveCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            var employee = await Repository.GetQueryable()
                .OfType<FemaleEmployee>()
                .Include(e => e.Pregnancies)
                .FirstOrDefaultAsync(e => e.Id == request.LeaveDto.EmployeeId, cancellationToken);

            if (employee == null)
            {
                return Result<PregnancyDto>.Failure("Employee not found");
            }

            var pregnancy = employee.Pregnancies.FirstOrDefault(p => p.Id == request.LeaveDto.PregnancyId);
            if (pregnancy == null)
            {
                return Result<PregnancyDto>.Failure("Pregnancy not found");
            }

            var leave = Leave.Create(request.LeaveDto.LeaveType, request.LeaveDto.DurationType,
                request.LeaveDto.Duration);

            pregnancy.Leaves.Add(leave);
            await UnitOfWork.CommitAsync();

            await _mediator.Publish(new EmployeeUpdatedEvent(employee.Id), cancellationToken);

            return Result<PregnancyDto>.Success(_mapper.Map<PregnancyDto>(pregnancy));
        }
    }
}
