using AutoMapper;
using Backend.Commands;
using Backend.DTOs;
using Backend.Entities;
using Backend.Events;
using Backend.Http;
using Backend.Repositories;
using FluentValidation;
using MediatR;

namespace Backend.Handlers
{
    public class CreateHolidayCommandHandler : BaseCommandHandler<CreateHolidayCommand, Result<HolidayDto>, Holiday>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IValidator<CreateHolidayCommand> _validator;

        public CreateHolidayCommandHandler(IGenericRepository<Holiday> repository, IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator, IValidator<CreateHolidayCommand> validator)
            : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _mediator = mediator;
            _validator = validator;
        }

        public override async Task<Result<HolidayDto>> Handle(CreateHolidayCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            var holiday = Holiday.Create(request.HolidayDto.Name, request.HolidayDto.Dates, request.HolidayDto.Description);

            await Repository.CreateAsync(holiday);
            await UnitOfWork.CommitAsync();

            await _mediator.Publish(new HolidayUpdatedEvent(), cancellationToken);

            return Result<HolidayDto>.Success(_mapper.Map<HolidayDto>(holiday));
        }
    }
}
