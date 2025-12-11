using CollaboratorEntity = Domain.Entities.Collaborator;

namespace Application.UseCases.Collaborator.Outputs;
internal class CollaboratorExtensions
{
    public static DefaultCollaboratorOutput? ToEventOutput(CollaboratorEntity entity)
    {
        if (entity == null) return null;

        return new DefaultCollaboratorOutput(
            entity.Id,
            entity.Name
        );
    }

    public static DetailedCollaboratorOutput? ToDetailedEventOutput(CollaboratorEntity entity)
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
