namespace ProfileNotifcationExample.Models.Stays;

public class StayTraveler : Traveler
{
    public DateTimeOffset? Arrival { get; set; }

    public DateTimeOffset? Departure { get; set; }

    public bool IsCustomer { get; set; }

    public Address Address { get; set; }
}
