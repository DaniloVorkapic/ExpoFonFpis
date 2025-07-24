using MediatR;

namespace Backend.Queries
{
    public abstract record BaseQuery<T> : IRequest<T>;
}
