namespace Domain.Contracts.Guest.Inputs;
public record CreateGuestInput(
    string Name,
    string Email,
    string PhoneNumber
);
