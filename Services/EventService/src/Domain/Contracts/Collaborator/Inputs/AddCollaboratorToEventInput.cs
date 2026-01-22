using static Domain.Entities.EventCollaborator;

namespace Domain.Contracts.Collaborator.Inputs;
public record AddCollaboratorToEventInput
(
    int CollaboratorId,
    int EventId,
    CollaboratorRole Role
);
