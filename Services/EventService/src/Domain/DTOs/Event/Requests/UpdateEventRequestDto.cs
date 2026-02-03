namespace Domain.DTOs.Event.Requests;
public record UpdateEventRequestDto
(
    int Id,
    string Name,
    string Description,
    string Location,
    DateTime StartDate,
    int OwnerUserId
);
