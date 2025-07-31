using MediatR;
using Warehouse.Domain.Enums;

namespace Warehouse.Application.Commands.Clients.EditClient;

public record EditClientCommand : IRequest
{
    public required long Id { get; init; }
    public required string Name { get; init; }
    public required string Address { get; init; }
    public Status Status { get; init; } = Status.Work;
}
