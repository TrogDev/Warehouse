using MediatR;

namespace Warehouse.Application.Commands.Outgoings.DeleteOutgoing;

public record DeleteOutgoingCommand : IRequest
{
    public required long Id { get; init; }
}
