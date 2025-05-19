namespace ProfileNotifcationExample.Models;

public class PartnerData : ProfileEntity
{
    public string Name { get; set; }

    public string MemberId { get; set; }

    public IEnumerable<PropertyValue> AdditionalProperty { get; set; }

}
