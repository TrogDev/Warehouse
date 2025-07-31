using MediatR;

namespace Warehouse.Application.Commands.Clients.DeleteClient;

public record DeleteClientCommand : IRequest
{
    public required long Id { get; init; }
}
