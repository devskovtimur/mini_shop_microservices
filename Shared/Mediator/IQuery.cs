using MediatR;

namespace Shared.Mediator;

public interface IQuery<T> : IRequest<Result<T>>;