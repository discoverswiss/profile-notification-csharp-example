namespace ProfileNotifcationExample.Models;

public class Party : ProfileEntity
{
    public string Identifier { get; set; }

    public string Name { get; set; }

    public string AdditionalType { get; set; }

    public IEnumerable<Traveler> Member { get; set; }
}
