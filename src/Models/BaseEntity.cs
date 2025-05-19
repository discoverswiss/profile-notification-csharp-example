namespace ProfileNotifcationExample.Models;

public class BaseEntity
{
    public DateTimeOffset? CreatedDateTime { get; set; }

    public DateTimeOffset? LastModified { get; set; }

    public string LastModifiedBy { get; set; }

    public string CreatedBySubscription { get; set; }

    public string LastModifiedBySubscription { get; set; }
}
