using MediatR;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Queries.Clients.GetClient;

public record GetClientQuery : IRequest<Client>
{
    public required long Id { get; init; }
}
