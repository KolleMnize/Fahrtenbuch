using ErrorOr;

namespace Fahrtenbuch.Application.Interfaces;

public interface ICommandHandler<in TCommand>
    where TCommand : ICommand
{
    Task<ErrorOr<Success>> Handle(TCommand command, CancellationToken ct = default);
}

public interface ICommandHandler<in TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
    Task<ErrorOr<TResponse>> Handle(TCommand command, CancellationToken ct = default);
}