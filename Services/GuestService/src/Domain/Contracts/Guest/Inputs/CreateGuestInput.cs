namespace Domain.Contracts.Guest.Inputs;
public record CreateGuestInput(
    int EventId,
    string Name,
    string Email,
    string PhoneNumber
);
