using MediatR;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Queries.Outgoings.GetOutgoing;

public record GetOutgoingQuery : IRequest<Outgoing>
{
    public required long Id { get; init; }
}
