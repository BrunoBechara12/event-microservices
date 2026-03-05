namespace Domain.Events;
 
public record EventUpdated(
    int EventId,
    string EventName,
    string UpdateMessage
);
