using Backend.Commands;
using Backend.Entities;
using Backend.Http;
using Backend.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Backend.Handlers
{
    public class DeleteChildCommandHandler : BaseCommandHandler<DeleteChildCommand, Result<Unit>, Employee>
    {
        public DeleteChildCommandHandler(IGenericRepository<Employee> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork) { }

        public override async Task<Result<Unit>> Handle(DeleteChildCommand request, CancellationToken cancellationToken)
        {
            var employee = await Repository.GetQueryable()
                .Include(e => e.Children)
                .FirstOrDefaultAsync(e => e.Id == request.EmployeeId, cancellationToken: cancellationToken); 

            if (employee == null)
            {
                return Result<Unit>.Failure("Employee not found.");
            }

            var childToRemove = employee.Children.FirstOrDefault(c => c.Id == request.ChildId);
            if (childToRemove == null)
            {
                return Result<Unit>.Failure("Child not found for the given employee.");
            }

            employee.Children.Remove(childToRemove);
            await UnitOfWork.CommitAsync();

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
