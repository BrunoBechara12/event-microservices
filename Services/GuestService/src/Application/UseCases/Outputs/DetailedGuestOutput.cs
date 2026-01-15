using static Domain.Entities.Guest;

namespace Application.UseCases.Outputs;
public record DetailedGuestOutput
(
    int Id,
    string Name, 
    string Email,
    string PhoneNumber,
    InviteStatus Status,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    DateTime? ResponseDate
);
