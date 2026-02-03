namespace Domain.DTOs.Collaborator.Requests;
public record RemoveCollaboratorFromEventRequestDto
(
    int CollaboratorId,
    int EventId
);
