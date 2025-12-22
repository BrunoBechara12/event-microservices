namespace Application.UseCases.Collaborator.Inputs;
public record UpdateCollaboratorInput
(
    int Id,
    int UserId,
    string Name
);
