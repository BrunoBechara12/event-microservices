namespace Domain.Events;

public record GuestInvited(
    int GuestId,
    string GuestName,
    string GuestPhoneNumber,
    int EventId,
    string EventName,
    DateTime EventDate
);
