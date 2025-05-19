namespace ProfileNotifcationExample.Models.Orders;

public class ProductConfiguration
{
    public ProductVariant Product { get; set; }

    public int? NumberOfTravelers { get; set; }

    public DateTimeOffset? ValidFrom { get; set; }

    public DateTimeOffset? validUntil { get; set; }
}
