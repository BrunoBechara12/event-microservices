namespace Domain.Events;

public record GuestConfirmed(
    int GuestId,
    string GuestName,
    string GuestPhoneNumber,
    int EventId,
    string EventName,
    bool IsAttending
);
