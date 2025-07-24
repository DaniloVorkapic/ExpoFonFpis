using Ardalis.Result;
using MediatR;

namespace Backend.Commands
{
    public abstract record BaseCommand<T> : IRequest<T>;
}
