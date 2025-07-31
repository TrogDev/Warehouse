using Warehouse.Application.DTO;

namespace Warehouse.Web.Api.Requests;

public record EditOutgoingBody
{
    public bool IsSigned { get; init; } = false;
    public required long ClientId { get; init; }
    public required string Number { get; init; }
    public required DateOnly Date { get; init; }
    public ICollection<CreateOrderItemData> Items { get; init; } = [];
}
