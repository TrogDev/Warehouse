using MediatR;
using Warehouse.Application.DTO;

namespace Warehouse.Application.Commands.Incomings.EditIncoming;

public record EditIncomingCommand : IRequest
{
    public required long Id { get; init; }
    public required string Number { get; set; }
    public required DateOnly Date { get; set; }
    public IEnumerable<CreateOrderItemData> Items { get; set; } = [];
}
