using MediatR;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Queries.Outgoings.GetOutgoingList;

public record GetOutgoingListQuery : IRequest<IEnumerable<Outgoing>>
{
    public DateOnly? DateFrom { get; init; }
    public DateOnly? DateTo { get; init; }
    public List<long>? Resources { get; init; }
    public List<long>? Units { get; init; }
    public List<long>? Clients { get; init; }
    public List<string>? Numbers { get; init; }
}
