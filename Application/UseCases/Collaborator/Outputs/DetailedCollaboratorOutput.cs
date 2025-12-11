namespace Application.UseCases.Collaborator.Outputs;
public record DetailedCollaboratorOutput
(
    int Id,
    string Name,
    int UserId,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
