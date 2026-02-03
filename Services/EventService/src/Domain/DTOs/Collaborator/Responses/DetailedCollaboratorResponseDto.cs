namespace Domain.DTOs.Collaborator.Responses;
public record DetailedCollaboratorResponseDto
(
    int Id,
    string Name,
    int UserId,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
