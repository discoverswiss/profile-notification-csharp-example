using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using ProfileNotifcationExample.Models;
using ProfileNotifcationExample.Models.Orders;
using ProfileNotifcationExample.Models.Stays;

namespace ProfileNotifcationExample;

public class ReceiverFunction
{
    // Event types (triggers) that represent operations or state changes in the system
    private const string CreateTrigger = "Create";
    private const string UpdateTrigger = "Update";
    private const string MergeTrigger = "Merge"; // Person-specific event
    private const string DeleteTrigger = "Delete";
    private const string DepositTrigger = "Deposit";

    // For Order entities, the eventTrigger corresponds to the order's status.
    // Full list of supported statuses: https://docs.discover.swiss/dev/concepts/order-statuses/#order-statuses-and-changes
    private const string FulfilledTrigger = "Fulfilled";
    private const string CanceledTrigger = "Canceled";

    // Supported entity types that can be processed by this function
    private const string OrderEntity = "Order";
    private const string TicketEntity = "Ticket";
    private const string PersonEntity = "Person";
    private const string PartyEntity = "Party";
    private const string StayEntity = "Stay";
    private const string PartnerDataEntity = "PartnerData";
    private const string AddressEntity = "Address";

    private readonly ILogger<ReceiverFunction> _logger;

    public ReceiverFunction(ILogger<ReceiverFunction> logger)
    {
        _logger = logger;
    }

    [Function(nameof(ReceiverFunction))]
    public async Task Run(
        [ServiceBusTrigger("test-demo-example", Connection = "ServiceBusConnectionString")]
        ServiceBusReceivedMessage message,
        ServiceBusMessageActions messageActions)
    {
        _logger.LogInformation("Received message with ID: {id}", message.MessageId);

        // The unique identifier for the profile associated with this event
        var profileId = message.ApplicationProperties.TryGetValue("ProfileId", out var parsedProfileId) ? parsedProfileId as string : null;
        // Internal identifier (not exposed via public APIs). Can be used in combination with profileId to uniquely identify the related object within the profile context.
        var identifer = message.ApplicationProperties.TryGetValue("Identifier", out var parsedIdentifer) ? parsedIdentifer as string : null;

        var entityName = message.ApplicationProperties.TryGetValue("EntityName", out var parsedEntityName) ? parsedEntityName as string : null;
        var eventTrigger = message.ApplicationProperties.TryGetValue("EventTrigger", out var parsedEventTrigger) ? parsedEventTrigger as string : null;

        // Additional metadata (optional): generally not processed, but may be useful for diagnostics or future extensions
        var partner = message.ApplicationProperties.TryGetValue("Partner", out var parsedPartner) ? parsedPartner as string : null;
        var datasource = message.ApplicationProperties.TryGetValue("Datasource", out var parsedDatasource) ? parsedDatasource as string : null;
        var lastModifiedBy = message.ApplicationProperties.TryGetValue("LastModifiedBy", out var parsedLastModifiedBy) ? parsedLastModifiedBy as string : null;

        // Defensive check — these properties should always be present due to contract requirements, but null-guarding just in case
        if (profileId == null || identifer == null || entityName == null || eventTrigger == null)
        {
            return;
        }

        switch (entityName)
        {
            case OrderEntity:
                HandleOrderMessage(message.Body, eventTrigger, profileId, message);
                break;
            case TicketEntity:
                HandleTicketMessage(message.Body, eventTrigger, profileId);
                break;
            case PersonEntity:
                HandlePersonMessage(message.Body, eventTrigger, profileId, message);
                break;
            case PartyEntity:
                HandlePartyMessage(message.Body, eventTrigger, profileId);
                break;
            case StayEntity:
                HandleStayMessage(message.Body, eventTrigger, profileId);
                break;
            case PartnerDataEntity:
                HandlePartnerDataMessage(message.Body, eventTrigger, profileId);
                break;
            case AddressEntity:
                HandleAddressMessage(message.Body, eventTrigger, profileId);
                break;
            default:
                _logger.LogWarning("Unsupported entity name: {EntityName}", entityName);
                break;
        }

        await messageActions.CompleteMessageAsync(message);
    }

    private void HandlePersonMessage(BinaryData body, string eventTrigger, string profileId, ServiceBusReceivedMessage message)
    {
        var person = body.ToObjectFromJson<Person>();
        if (person == null) return;

        _logger.LogDebug("Received message: {Person}", JsonSerializer.Serialize(person));

        switch (eventTrigger)
        {
            case CreateTrigger:
                _logger.LogInformation("Received person create message for profile {ProfileId}. Message Content: {Person}", profileId, JsonSerializer.Serialize(person));

                // Option to differentiate between a guest and an authorized user
                var isGuest = "Guest".Equals(person.IdentificationLevel);
                if (isGuest)
                {
                    _logger.LogInformation("A new guest profile was created");
                }
                break;
            case UpdateTrigger:
                _logger.LogInformation("Received person update message for profile {ProfileId}.", profileId);
                break;
            case MergeTrigger:
                _logger.LogInformation("Received person merge message for profile {ProfileId}.", profileId);

                // Note: A separate update event is sent for the new profile, and a delete event is sent for the old one
                var sourceProfileId = message.ApplicationProperties.TryGetValue("SourceProfileId", out var parsedSourceProfileId) ? parsedSourceProfileId as string : null;
                _logger.LogInformation("Data from Profile {SourceProfileId} merged into {ProfileId}", sourceProfileId, profileId);
                break;
            case DeleteTrigger:
                // Partners must listen for Person Delete events and remove related data from their systems to ensure compliance with data protection regulations
                _logger.LogInformation("Received person delete message for profile {ProfileId}.", profileId);
                break;
        }
    }

    private void HandlePartyMessage(BinaryData body, string eventTrigger, string profileId)
    {
        var party = body.ToObjectFromJson<Party>();
        if (party == null) return;

        _logger.LogDebug("Received message: {Party}", JsonSerializer.Serialize(party));

        switch (eventTrigger)
        {
            case CreateTrigger:
                _logger.LogInformation("Received party create message for profile {ProfileId}.", profileId);
                break;
            case UpdateTrigger:
                _logger.LogInformation("Received party update message for profile {ProfileId}.", profileId);
                break;
            case DeleteTrigger:
                // if the home address (the main address of the person) is deleted then an extra person update message will we sent
                _logger.LogInformation("Received party delete message for profile {ProfileId}.", profileId);
                break;
        }
    }

    private void HandleStayMessage(BinaryData body, string eventTrigger, string profileId)
    {
        var stay = body.ToObjectFromJson<Stay>();
        if (stay == null) return;

        _logger.LogDebug("Received message: {Stay}", JsonSerializer.Serialize(stay));

        switch (eventTrigger)
        {
            case CreateTrigger:
                _logger.LogInformation("Received stay create message for profile {ProfileId}.", profileId);
                break;
            case UpdateTrigger:
                _logger.LogInformation("Received stay update message for profile {ProfileId}.", profileId);
                break;
            case DeleteTrigger:
                _logger.LogInformation("Received stay delete message for profile {ProfileId}.", profileId);
                break;
        }
    }

    private void HandlePartnerDataMessage(BinaryData body, string eventTrigger, string profileId)
    {
        var partnerData = body.ToObjectFromJson<PartnerData>();
        if (partnerData == null) return;

        _logger.LogDebug("Received message: {PartnerData}", JsonSerializer.Serialize(partnerData));

        switch (eventTrigger)
        {
            case CreateTrigger:
                _logger.LogInformation("Received partnerData create message for profile {ProfileId}.", profileId);
                break;
            case UpdateTrigger:
                _logger.LogInformation("Received partnerData update message for profile {ProfileId}.", profileId);
                break;
            case DeleteTrigger:
                _logger.LogInformation("Received partnerData delete message for profile {ProfileId}.", profileId);
                break;
        }
    }

    private void HandleAddressMessage(BinaryData body, string eventTrigger, string profileId)
    {
        var address = body.ToObjectFromJson<Address>();
        if (address == null) return;

        _logger.LogDebug("Received message: {Address}", JsonSerializer.Serialize(address));

        switch (eventTrigger)
        {
            case CreateTrigger:
                _logger.LogInformation("Received address create message for profile {ProfileId}.", profileId);
                break;
            case UpdateTrigger:
                _logger.LogInformation("Received address update message for profile {ProfileId}.", profileId);
                break;
            case DeleteTrigger:
                _logger.LogInformation("Received address delete message for profile {ProfileId}.", profileId);
                break;
        }
    }

    private void HandleOrderMessage(BinaryData body, string eventTrigger, string profileId, ServiceBusReceivedMessage message)
    {
        var order = body.ToObjectFromJson<Order>();
        if (order == null) return;

        _logger.LogDebug("Received message: {Order}", JsonSerializer.Serialize(order));

        // Optional: can be used to distinguish between B2C and B2B transactions
        var salesChannel = message.ApplicationProperties.TryGetValue("SalesChannel", out var parsedSalesChannel) ? parsedSalesChannel as string : null;

        switch (eventTrigger)
        {
            case FulfilledTrigger:
                _logger.LogInformation("Order {OrderNumber} fulfilled. Common event trigger to add it to the CRM.", order.OrderNumber);
                break;
            case CanceledTrigger:
                _logger.LogInformation("Order {OrderNumber} canceled", order.OrderNumber);
                break;
            default:
                _logger.LogInformation("Order {OrderNumber} triggered with status: {EventTrigger}", order.OrderNumber, eventTrigger);
                break;
        }
    }

    private void HandleTicketMessage(BinaryData body, string eventTrigger, string profileId)
    {
        var ticket = body.ToObjectFromJson<Ticket>();
        if (ticket == null) return;

        _logger.LogDebug("Received message: {Ticket}", JsonSerializer.Serialize(ticket));

        switch (eventTrigger)
        {
            case CreateTrigger:
                _logger.LogInformation("Received ticket create message for profile {ProfileId}.", profileId);
                break;
            case UpdateTrigger:
                // Note: A canceled ticket has its 'active' property set to false.
                _logger.LogInformation("Received ticket update message for profile {ProfileId}.", profileId);
                break;
            case DepositTrigger:
                _logger.LogInformation("Received ticket deposit message for profile {ProfileId}.", profileId);
                break;
            case DeleteTrigger:
                _logger.LogInformation("Received ticket delete message for profile {ProfileId}.", profileId);
                break;
        }
    }
}
