using Warehouse.Domain.Enums;

namespace Warehouse.Domain.Entities;

public class Unit : BaseEntity<long>
{
    public string Name { get; set; }
    public Status Status { get; set; } = Status.Work;
}
