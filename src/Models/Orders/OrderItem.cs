namespace ProfileNotifcationExample.Models.Orders;

public class OrderItem
{
    public string OrderItemNumber { get; set; }

    public string ParentOrderItemNumber { get; set; }

    public OrderItemDelivery OrderItemDelivery { get; set; }

    public string OrderItemStatus { get; set; }

    public int OrderQuantity { get; set; }

    public ProductConfiguration OrderedItem { get; set; }

    public decimal Amount { get; set; }

    public decimal AmountCHF { get; set; }

    public IEnumerable<Ticket> Ticket { get; set; }

    public IEnumerable<PropertyValue> AdditionalProperty { get; set; } = new List<PropertyValue>();
}
