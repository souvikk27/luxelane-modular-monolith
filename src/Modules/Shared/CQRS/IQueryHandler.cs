using Shared.Abstraction;

namespace Shared.CQRS;

public interface IQueryHandler<TQuery, TResult> : IRequestHandler<TQuery, Result<TResult>>
    where TQuery : IQuery<TResult>
{
}