using static Domain.Entities.Guest;

namespace Domain.DTOs.Guest.Responses;
public record DetailedGuestResponseDto
(
    int Id,
    int EventId,
    string Name,
    string Email,
    string PhoneNumber,
    InviteStatus Status,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    DateTime? ResponseDate
);
