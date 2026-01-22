namespace Domain.Contracts.Guest.Inputs;
public record UpdateGuestInput(
    int Id,
    string Name,
    string Email,
    string PhoneNumber
);
