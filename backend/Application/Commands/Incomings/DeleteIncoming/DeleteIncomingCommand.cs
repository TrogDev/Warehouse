using MediatR;

namespace Warehouse.Application.Commands.Incomings.DeleteIncoming;

public record DeleteIncomingCommand : IRequest
{
    public required long Id { get; init; }
}
