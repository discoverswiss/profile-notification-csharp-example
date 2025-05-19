using ProfileNotifcationExample.Models.Orders;

namespace ProfileNotifcationExample.Models.Stays;

public class Stay : ProfileEntity
{
    public string Id { get; set; }

    public string Status { get; set; }

    public string Name { get; set; }

    public LodgingBusiness LodgingBusiness { get; set; }

    public Person Customer { get; set; }

    public IEnumerable<StayTraveler> Member { get; set; }

    public IEnumerable<StayTravelerGroup> Group { get; set; }

    public DateTimeOffset? Arrival { get; set; }

    public DateTimeOffset? Departure { get; set; }

    public IEnumerable<PropertyValue> AdditionalProperty { get; set; }

    public IEnumerable<Ticket> Ticket { get; set; }

    public string Language { get; set; }
}
