using MediatR;

namespace Warehouse.Application.Queries.Units.GetUnitList;

public record GetUnitListQuery : IRequest<IEnumerable<Domain.Entities.Unit>> { }
