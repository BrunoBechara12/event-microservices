namespace Domain.DTOs.Collaborator.Requests;
public record UpdateCollaboratorRequestDto
(
    int Id,
    int UserId,
    string Name
);
