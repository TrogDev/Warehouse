using MediatR;

namespace Warehouse.Application.Commands.Units.DeleteUnit;

public record DeleteUnitCommand : IRequest
{
    public required long Id { get; init; }
}
