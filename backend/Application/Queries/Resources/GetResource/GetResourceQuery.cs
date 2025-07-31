using MediatR;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Queries.Resources.GetResource;

public record GetResourceQuery : IRequest<Resource>
{
    public required long Id { get; init; }
}
