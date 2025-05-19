namespace ProfileNotifcationExample.Models.Orders;

public class Order : ProfileEntity
{
    public string OrderNumber { get; set; }

    public string OrderTokenName { get; set; }

    public DateTimeOffset? OrderDate { get; set; }

    public Person Customer { get; set; }

    public OrderAddress BillingAddress { get; set; }

    public OrderAddress ShippingAddress { get; set; }

    public string OrderStatus { get; set; }

    public string PriceCurrency { get; set; }

    public decimal? TotalAmount { get; set; }

    public decimal? TotalAmountCHF { get; set; }

    public List<OrderItem> OrderedItem { get; set; }

    public string Language { get; set; }

    public string Description { get; set; }

    public string MailBodyToken { get; set; }

    public string SalesChannel { get; set; }

    public string OrderPaymentStatus { get; set; }

    public IEnumerable<PropertyValue> AdditionalProperty { get; set; }
}
