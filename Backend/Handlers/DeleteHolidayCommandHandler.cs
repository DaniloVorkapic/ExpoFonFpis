using Backend.Commands;
using Backend.Entities;
using Backend.Events;
using Backend.Http;
using Backend.Repositories;
using MediatR;

namespace Backend.Handlers
{
    public class DeleteHolidayCommandHandler : BaseCommandHandler<DeleteHolidayCommand, Result<Unit>, Holiday>
    {
        private readonly IMediator _mediator;

        public DeleteHolidayCommandHandler(IGenericRepository<Holiday> repository, IUnitOfWork unitOfWork, IMediator mediator) : base(repository, unitOfWork)
        {
            _mediator = mediator;
        }

        public override async Task<Result<Unit>> Handle(DeleteHolidayCommand request, CancellationToken cancellationToken)
        {
            var holidayToDelete = await Repository.GetByIdAsync(request.HolidayId);

            if (holidayToDelete == null)
            {
                return Result<Unit>.Failure("Holiday not found");
            }

            await Repository.DeleteAsync(holidayToDelete);
            await UnitOfWork.CommitAsync();

            await _mediator.Publish(new HolidayUpdatedEvent(), cancellationToken);

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
