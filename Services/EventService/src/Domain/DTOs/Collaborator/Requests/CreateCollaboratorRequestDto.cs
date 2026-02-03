namespace Domain.DTOs.Collaborator.Requests;
public record CreateCollaboratorRequestDto
(
    int UserId,
    string Name
);
