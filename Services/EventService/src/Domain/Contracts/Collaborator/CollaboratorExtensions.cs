using Domain.Contracts.Collaborator.Outputs;
using CollaboratorEntity = Domain.Entities.Collaborator;

namespace Domain.Contracts.Collaborator;

public static class CollaboratorExtensions
{
    public static DefaultCollaboratorOutput? ToDefaultCollaboratorOutput(this CollaboratorEntity entity)
    {
        if (entity == null) return null;

        return new DefaultCollaboratorOutput(
            entity.Id,
            entity.Name
        );
    }

    public static DetailedCollaboratorOutput? ToDetailedCollaboratorOutput(this CollaboratorEntity entity)
    {
        if (entity == null) return null;

        return new DetailedCollaboratorOutput(
            entity.Id,
            entity.Name,
            entity.UserId,
            entity.CreatedAt,
            entity.UpdatedAt
        );
    }
}
