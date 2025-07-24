using Backend.Entities;
using Backend.Repositories;
using MediatR;

namespace Backend.Handlers
{
    public abstract class BaseCommandHandler<TRequest, TResponse, TEntity> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TEntity : BaseEntity
    {
        protected readonly IGenericRepository<TEntity> Repository;
        protected readonly IUnitOfWork UnitOfWork;
        protected BaseCommandHandler(IGenericRepository<TEntity> repository, IUnitOfWork unitOfWork)
        {
            Repository = repository;
            UnitOfWork = unitOfWork;
        }
        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
