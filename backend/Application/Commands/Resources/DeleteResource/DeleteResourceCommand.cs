using MediatR;

namespace Warehouse.Application.Commands.Resources.DeleteResource;

public record DeleteResourceCommand : IRequest
{
    public required long Id { get; init; }
}
