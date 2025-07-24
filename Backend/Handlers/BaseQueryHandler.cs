using AutoMapper;
using Backend.Entities;
using Backend.Repositories;
using MediatR;

namespace Backend.Handlers
{
    public abstract class BaseQueryHandler<TRequest, TResponse, TEntity> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TEntity : BaseEntity
    {
        protected readonly IGenericRepository<TEntity> Repository;
        protected readonly IMapper Mapper;

        protected BaseQueryHandler(IGenericRepository<TEntity> repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
