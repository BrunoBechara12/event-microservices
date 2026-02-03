namespace Domain.DTOs.Guest.Requests;
public record CreateGuestRequestDto(
    int EventId,
    string Name,
    string Email,
    string PhoneNumber
);
