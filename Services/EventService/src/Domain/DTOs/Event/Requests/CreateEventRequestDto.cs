namespace Domain.DTOs.Event.Requests;
public record CreateEventRequestDto
(
    string Name,
    string Description,
    string Location,
    DateTime StartDate,
    int OwnerUserId
);
