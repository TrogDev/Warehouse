using System.Text.Json.Serialization;

namespace Warehouse.Domain.Entities;

public class OutgoingItem : ResourceUnitEntity<long>
{
    public int Quantity { get; set; }
    public long OutgoingId { get; set; }

    [JsonIgnore]
    public Outgoing Outgoing { get; set; }
}
