namespace Warehouse.Application.DTO;

public record CreateOrderItemData
{
    public required long ResourceId { get; init; }
    public required long UnitId { get; init; }
    public required int Quantity { get; init; }
}
