using MediatR;
using Warehouse.Application.DTO;

namespace Warehouse.Application.Commands.Incomings.CreateIncoming;

public record CreateIncomingCommand : IRequest
{
    public required string Number { get; set; }
    public required DateOnly Date { get; set; }
    public IEnumerable<CreateOrderItemData> Items { get; set; } = [];
}
