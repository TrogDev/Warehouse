using Warehouse.Application.DTO;

namespace Warehouse.Web.Api.Requests;

public record EditIncomingBody
{
    public required string Number { get; init; }
    public required DateOnly Date { get; init; }
    public ICollection<CreateOrderItemData> Items { get; init; } = [];
}
