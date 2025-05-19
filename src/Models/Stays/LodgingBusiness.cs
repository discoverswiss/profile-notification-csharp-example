namespace ProfileNotifcationExample.Models.Stays;

public class LodgingBusiness
{
    public string Id { get; set; }

    public string BranchCode { get; set; }

    public string Name { get; set; }

    public string AdditionalType { get; set; }

    public Address Address { get; set; }

    public ImageObject Image { get; set; }
}
