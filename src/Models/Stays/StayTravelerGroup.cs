namespace ProfileNotifcationExample.Models.Stays;

public class StayTravelerGroup
{
    public string Id { get; set; }

    public string Name { get; set; }

    public int NumberOfTravelers { get; set; }

    public IEnumerable<PropertyValue> AdditionalProperty { get; set; }
}
