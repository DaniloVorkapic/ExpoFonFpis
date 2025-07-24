using Backend.Commands;
using Backend.Entities;
using Backend.Events;
using Backend.Http;
using Backend.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Backend.Handlers
{
    public class DeletePregnancyCommandHandler : BaseCommandHandler<DeletePregnancyCommand, Result<Unit>, Employee>
    {
        private readonly IMediator _mediator;

        public DeletePregnancyCommandHandler(IGenericRepository<Employee> repository, IUnitOfWork unitOfWork, IMediator mediator) : base(repository, unitOfWork)
        {
            _mediator = mediator;
        }

        public override async Task<Result<Unit>> Handle(DeletePregnancyCommand request, CancellationToken cancellationToken)
        {
            var employee = await Repository.GetQueryable()
                .OfType<FemaleEmployee>()
                .Include(e => e.Pregnancies)
                .FirstOrDefaultAsync(e => e.Id == request.EmployeeId, cancellationToken);
            if (employee == null)
            {
                return Result<Unit>.Failure("Employee not found.");
            }

            var pregnancyToRemove = employee.Pregnancies.FirstOrDefault(p => p.Id == request.PregnancyId);
            if (pregnancyToRemove == null)
            {
                return Result<Unit>.Failure("Pregnancy not found.");
            }

            employee.Pregnancies.Remove(pregnancyToRemove);
            await UnitOfWork.CommitAsync();

            await _mediator.Publish(new EmployeeUpdatedEvent(employee.Id), cancellationToken);

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
