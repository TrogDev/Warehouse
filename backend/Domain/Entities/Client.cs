using Warehouse.Domain.Enums;

namespace Warehouse.Domain.Entities;

public class Client : BaseEntity<long>
{
    public string Name { get; set; }
    public string Address { get; set; }
    public Status Status { get; set; } = Status.Work;
}
