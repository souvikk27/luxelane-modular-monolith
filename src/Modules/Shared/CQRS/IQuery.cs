using Shared.Abstraction;

namespace Shared.CQRS;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}