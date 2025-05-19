namespace ProfileNotifcationExample.Models.Orders;

public class Ticket : ProfileEntity
{
    public string Id { get; set; }

    public string ParentTicketIdentifier { get; set; }

    public string OrderNumber { get; set; }

    public string OrderItemNumber { get; set; }

    public string Name { get; set; }

    public string AdditionalType { get; set; }

    public string TicketTokenId { get; set; }

    public string TicketNumber { get; set; }

    public string BookingNumber { get; set; }

    public string TicketToken { get; set; }

    public string HtmlToken { get; set; }

    public string ImgToken { get; set; }

    public DateTime? DateIssued { get; set; }

    public string PriceCurrency { get; set; }

    public decimal TotalPrice { get; set; }

    public decimal PriceCHF { get; set; }

    public Traveler UnderName { get; set; }

    public IEnumerable<Traveler> UnderNames { get; set; }

    public int? NumberOfTravelers { get; set; }

    public DateTimeOffset? ValidFrom { get; set; }

    public DateTimeOffset? ValidUntil { get; set; }

    public ProductVariant Product { get; set; }

    public string PriceCategory { get; set; }

    public string PriceCategoryName { get; set; }

    public string Reduction { get; set; }

    public IEnumerable<PropertyValue> AdditionalProperty { get; set; }

    public string TicketVerificationStatus { get; set; }

    public string TicketRenewStatus { get; set; }
}
