using MediatR;
using Warehouse.Domain.Enums;

namespace Warehouse.Application.Commands.Clients.CreateClient;

public record CreateClientCommand : IRequest
{
    public required string Name { get; init; }
    public required string Address { get; init; }
    public Status Status { get; init; } = Status.Work;
}
