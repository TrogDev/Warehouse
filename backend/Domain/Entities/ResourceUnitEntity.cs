namespace Warehouse.Domain.Entities;

public abstract class ResourceUnitEntity<TKey> : BaseEntity<TKey>
{
    public long ResourceId { get; set; }
    public Resource Resource { get; set; }
    public long UnitId { get; set; }
    public Unit Unit { get; set; }
}
