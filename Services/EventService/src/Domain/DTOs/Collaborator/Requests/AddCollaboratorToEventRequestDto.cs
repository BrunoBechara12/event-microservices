using static Domain.Entities.EventCollaborator;

namespace Domain.DTOs.Collaborator.Requests;
public record AddCollaboratorToEventRequestDto
(
    int CollaboratorId,
    int EventId,
    CollaboratorRole Role
);
