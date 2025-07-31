using MediatR;
using Warehouse.Domain.Enums;

namespace Warehouse.Application.Commands.Units.CreateUnit;

public record CreateUnitCommand : IRequest
{
    public required string Name { get; init; }
    public Status Status { get; init; } = Status.Work;
}
