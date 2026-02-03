namespace Domain.DTOs.Event.Responses;
public record DetailedEventResponseDto
(
    int Id,
    string Name,
    string Description,
    string Location,
    DateTime StartDate,
    int OwnerUserId,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
