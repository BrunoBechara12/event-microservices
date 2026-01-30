using static Domain.Entities.Guest;

namespace Domain.Contracts.Guest.Outputs;
public record DetailedGuestOutput
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
