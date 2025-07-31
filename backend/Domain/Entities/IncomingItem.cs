using System.Text.Json.Serialization;

namespace Warehouse.Domain.Entities;

public class IncomingItem : ResourceUnitEntity<long>
{
    public int Quantity { get; set; }
    public long IncomingId { get; set; }

    [JsonIgnore]
    public Incoming Incoming { get; set; }
}
