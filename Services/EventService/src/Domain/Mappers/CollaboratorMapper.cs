using Domain.DTOs.Collaborator.Responses;
using CollaboratorEntity = Domain.Entities.Collaborator;

namespace Domain.Mappers;

public static class CollaboratorMapper
{
    public static DefaultCollaboratorResponseDto? ToDefaultResponseDto(this CollaboratorEntity entity)
    {
        if (entity == null) return null;

        return new DefaultCollaboratorResponseDto(
            entity.Id,
            entity.Name
        );
    }

    public static DetailedCollaboratorResponseDto? ToDetailedResponseDto(this CollaboratorEntity entity)
    {
        if (entity == null) return null;

        return new DetailedCollaboratorResponseDto(
            entity.Id,
            entity.Name,
            entity.UserId,
            entity.CreatedAt,
            entity.UpdatedAt
        );
    }
}
