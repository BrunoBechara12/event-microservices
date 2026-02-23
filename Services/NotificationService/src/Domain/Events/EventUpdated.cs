namespace Domain.Events;

/// <summary>
/// Event received when an event is updated.
/// Triggers notifications to all guests about the update.
/// </summary>
public record EventUpdated(
    int EventId,
    string EventName,
    string UpdateMessage
);
