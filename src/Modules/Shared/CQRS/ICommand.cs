using Shared.Abstraction;

namespace Shared.CQRS;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand
{
}

public interface ICommand : IRequest<Result>, IBaseCommand
{
}