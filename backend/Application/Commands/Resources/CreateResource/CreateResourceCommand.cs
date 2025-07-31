using MediatR;
using Warehouse.Domain.Enums;

namespace Warehouse.Application.Commands.Resources.CreateResource;

public record CreateResourceCommand : IRequest
{
    public required string Name { get; init; }
    public Status Status { get; init; } = Status.Work;
}
