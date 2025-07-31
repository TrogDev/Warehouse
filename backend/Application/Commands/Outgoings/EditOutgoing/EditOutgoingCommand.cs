using MediatR;
using Warehouse.Application.DTO;

namespace Warehouse.Application.Commands.Outgoings.EditOutgoing;

public record EditOutgoingCommand : IRequest
{
    public required long Id { get; init; }
    public required string Number { get; set; }
    public required DateOnly Date { get; set; }
    public required long ClientId { get; init; }
    public bool IsSigned { get; set; } = false;
    public IEnumerable<CreateOrderItemData> Items { get; set; } = [];
}
