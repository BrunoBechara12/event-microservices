namespace Application.UseCases.Inputs;
public record UpdateGuestInput(
    int Id,
    string Name,
    string Email,
    string PhoneNumber
);
