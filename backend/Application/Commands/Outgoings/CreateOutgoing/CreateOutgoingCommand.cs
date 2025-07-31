using MediatR;
using Warehouse.Application.DTO;

namespace Warehouse.Application.Commands.Outgoings.CreateOutgoing;

public record CreateOutgoingCommand : IRequest
{
    public required string Number { get; set; }
    public required DateOnly Date { get; set; }
    public required long ClientId { get; init; }
    public bool IsSigned { get; set; } = false;
    public IEnumerable<CreateOrderItemData> Items { get; set; } = [];
}
