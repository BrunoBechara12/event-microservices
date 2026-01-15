namespace Application.UseCases.Inputs;
public record CreateGuestInput(
    string Name,
    string Email,
    string PhoneNumber
);
