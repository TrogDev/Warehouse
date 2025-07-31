using MediatR;
using Warehouse.Domain.Enums;

namespace Warehouse.Application.Commands.Resources.EditResource;

public record EditResourceCommand : IRequest
{
    public required long Id { get; init; }
    public required string Name { get; init; }
    public Status Status { get; init; } = Status.Work;
}
