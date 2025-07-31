using MediatR;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Queries.Incomings.GetIncoming;

public record GetIncomingQuery : IRequest<Incoming>
{
    public required long Id { get; init; }
}
