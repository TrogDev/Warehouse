namespace Warehouse.Domain.Entities;

public class Incoming : BaseEntity<long>
{
    public string Number { get; set; }
    public DateOnly Date { get; set; }
    public ICollection<IncomingItem> Items { get; set; } = [];
}
