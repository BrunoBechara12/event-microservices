namespace Domain.Contracts.Collaborator.Inputs;
public record RemoveCollaboratorFromEventInput
(
    int CollaboratorId,
    int EventId
);
