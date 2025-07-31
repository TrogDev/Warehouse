namespace Warehouse.Domain.Entities;

public class Outgoing : BaseEntity<long>
{
    public string Number { get; set; }
    public DateOnly Date { get; set; }
    public bool IsSigned { get; set; } = false;
    public long ClientId { get; set; }
    public Client Client { get; set; }
    public ICollection<OutgoingItem> Items { get; set; } = [];
}
