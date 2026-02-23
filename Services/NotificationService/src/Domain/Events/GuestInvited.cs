namespace Domain.Events;

/// <summary>
/// Event received when a guest is invited to an event.
/// Triggers a WhatsApp notification to the guest.
/// </summary>
public record GuestInvited(
    int GuestId,
    string GuestName,
    string GuestPhoneNumber,
    int EventId,
    string EventName,
    DateTime EventDate
);
