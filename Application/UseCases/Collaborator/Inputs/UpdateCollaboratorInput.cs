namespace Application.UseCases.Collaborator.Inputs;
public record UpdateCollaboratorInput
(
    int Id,
    string Name,
    int UserId 
);
