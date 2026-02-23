namespace Domain.Events;

/// <summary>
/// Event received when a guest confirms attendance to an event.
/// Can trigger a confirmation notification.
/// </summary>
public record GuestConfirmed(
    int GuestId,
    string GuestName,
    string GuestPhoneNumber,
    int EventId,
    string EventName,
    bool IsAttending
);
