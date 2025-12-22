using static Domain.Entities.EventCollaborator;

namespace Application.UseCases.Collaborator.Inputs;
public record AddCollaboratorToEventInput
(
    int CollaboratorId,
    int EventId,
    CollaboratorRole Role
);
