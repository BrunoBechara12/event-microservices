namespace Domain.DTOs.Guest.Requests;
public record UpdateGuestRequestDto(
    int Id,
    string Name,
    string Email,
    string PhoneNumber
);
