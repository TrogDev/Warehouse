using MediatR;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Queries.Incomings.GetIncomingList;

public record GetIncomingListQuery : IRequest<IEnumerable<Incoming>>
{
    public DateOnly? DateFrom { get; init; }
    public DateOnly? DateTo { get; init; }
    public List<long>? Resources { get; init; }
    public List<long>? Units { get; init; }
    public List<string>? Numbers { get; init; }
}
