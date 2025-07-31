using MediatR;

namespace Warehouse.Application.Queries.Units.GetUnit;

public record GetUnitQuery : IRequest<Domain.Entities.Unit>
{
    public required long Id { get; init; }
}
