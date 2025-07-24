using Backend.Commands;
using Backend.Entities;
using Backend.Events;
using Backend.Http;
using Backend.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Backend.Handlers
{
    public class DeleteLeaveCommandHandler : BaseCommandHandler<DeleteLeaveCommand, Result<Unit>, Employee>
    {
        private readonly IMediator _mediator;

        public DeleteLeaveCommandHandler(IGenericRepository<Employee> repository, IUnitOfWork unitOfWork, IMediator mediator) : base(repository, unitOfWork)
        {
            _mediator = mediator;
        }

        public override async Task<Result<Unit>> Handle(DeleteLeaveCommand request, CancellationToken cancellationToken)
        {
            var employee = await Repository.GetQueryable()
                .OfType<FemaleEmployee>()
                .Include(e => e.Pregnancies)
                .ThenInclude(p => p.Leaves)
                .FirstOrDefaultAsync(e => e.Id == request.EmployeeId, cancellationToken);

            if (employee == null)
            {
                return Result<Unit>.Failure("Employee not found.");
            }

            var pregnancy = employee.Pregnancies.FirstOrDefault(p => p.Id == request.PregnancyId);
            if (pregnancy == null)
            {
                return Result<Unit>.Failure("Pregnancy not found.");
            }

            var leave = pregnancy.Leaves.FirstOrDefault(l => l.Id == request.LeaveId);
            if (leave == null)
            {
                return Result<Unit>.Failure("Leave not found.");
            }

            pregnancy.Leaves.Remove(leave);
            await UnitOfWork.CommitAsync();

            await _mediator.Publish(new EmployeeUpdatedEvent(employee.Id), cancellationToken);

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
