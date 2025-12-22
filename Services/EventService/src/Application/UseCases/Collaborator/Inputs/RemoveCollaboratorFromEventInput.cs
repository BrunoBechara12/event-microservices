namespace Application.UseCases.Collaborator.Inputs;
public record RemoveCollaboratorFromEventInput
(
    int CollaboratorId,
    int EventId
);
